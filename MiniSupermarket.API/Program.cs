using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// 1. Cấu hình CORS cho phép WinForms / Client gọi API (Tiêu chí 11)
// ========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ========================================
// 2. Cấu hình xác thực JWT (Tiêu chí 28, 29, 30)
// ========================================
var jwtSecret = builder.Configuration["JwtSettings:Secret"]
    ?? throw new InvalidOperationException("Thiếu cấu hình JwtSettings:Secret trong appsettings.json!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(jwtSecret)
        ),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

// ========================================
// 3. Đăng ký DbContext lấy chuỗi kết nối từ appsettings.json (Tiêu chí 12 & 19)
// ========================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Thiếu chuỗi kết nối DefaultConnection trong appsettings.json!");

builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// ========================================
// 4. Đăng ký Controllers + Swagger UI kèm nút Authorize JWT (Tiêu chí 11, 17, 31)
// ========================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT token theo dạng: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ========================================
// 5. Tự động đồng bộ dữ liệu mẫu (Seed Data) khi khởi động (Tiêu chí 22)
// ========================================
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<SupermarketDbContext>();

        // Bổ sung các Brand mẫu mới nếu chưa có trong CSDL
        var existingBrandIds = await db.Brands.Select(b => b.BrandId).ToListAsync();
        var extraBrands = new[]
        {
            new Brand { BrandId = 6, BrandName = "Orion", Description = "Thương hiệu bánh kẹo và thực phẩm đóng gói Orion" },
            new Brand { BrandId = 7, BrandName = "Coca-Cola", Description = "Thương hiệu nước giải khát toàn cầu" },
            new Brand { BrandId = 8, BrandName = "Samsung", Description = "Thiết bị điện tử và gia dụng thông minh Samsung SmartThings" }
        };
        foreach (var b in extraBrands)
        {
            if (!existingBrandIds.Contains(b.BrandId))
                db.Brands.Add(b);
        }
        await db.SaveChangesAsync();

        // Bổ sung 5 sản phẩm mẫu (ID 1..5) nếu chưa có trong CSDL
        var existingProductIds = await db.Products.Select(p => p.ProductId).ToListAsync();
        var extraProducts = new[]
        {
            new Product { ProductId = 1, Barcode = "893456789001", ProductName = "Snack Khoai Tây O'Star Gói 60g", Price = 12500m, StockQuantity = 150, CategoryId = 7, BrandId = 6 },
            new Product { ProductId = 2, Barcode = "893456789002", ProductName = "Bánh Quy Bơ Danisa Hộp Thiếc 454g", Price = 135000m, StockQuantity = 40, CategoryId = 7, BrandId = 6 },
            new Product { ProductId = 3, Barcode = "893456789003", ProductName = "Kẹo Dẻo Chupa Chups Hương Trái Cây", Price = 15000m, StockQuantity = 120, CategoryId = 7, BrandId = 6 },
            new Product { ProductId = 4, Barcode = "893456789004", ProductName = "Bánh Chocopie Orion Hộp 12 Cái", Price = 54000m, StockQuantity = 60, CategoryId = 7, BrandId = 6 },
            new Product { ProductId = 5, Barcode = "893456789005", ProductName = "Nước Ngọt Coca Cola Lon 320ml", Price = 10000m, StockQuantity = 320, CategoryId = 7, BrandId = 7 }
        };
        foreach (var p in extraProducts)
        {
            if (!existingProductIds.Contains(p.ProductId))
                db.Products.Add(p);
        }
        await db.SaveChangesAsync();
    }
    catch
    {
        // Bỏ qua nếu CSDL chưa bật khi build
    }
}

// ========================================
// 6. Middleware Pipeline
// ========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
