using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tiêu chí 29: Chặn API khi chưa đăng nhập (trả về 401 Unauthorized)
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // Hàm ánh xạ Entity Product sang ProductItemDto (Tiêu chí 15 & kết nối quan hệ Category + Brand)
        private static ProductItemDto MapToDto(Product p)
        {
            return new ProductItemDto
            {
                ProductId = p.ProductId,
                Barcode = p.Barcode,
                ProductName = p.ProductName,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.CategoryName : "Chưa phân loại",
                BrandId = p.BrandId,
                BrandName = p.Brand != null ? p.Brand.BrandName : "Chưa rõ thương hiệu",
                Category = p.Category != null ? new CategoryDto
                {
                    CategoryId = p.Category.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    Description = p.Category.Description
                } : null,
                Brand = p.Brand != null ? new BrandDto
                {
                    BrandId = p.Brand.BrandId,
                    BrandName = p.Brand.BrandName,
                    Description = p.Brand.Description,
                    LogoUrl = p.Brand.LogoUrl
                } : null
            };
        }

        // 1. READ: Lấy toàn bộ danh sách sản phẩm kèm thông tin Nhóm hàng (Category) và Thương hiệu (Brand)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId = null, [FromQuery] int? brandId = null)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsNoTracking()
                .AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (brandId.HasValue && brandId.Value > 0)
            {
                query = query.Where(p => p.BrandId == brandId.Value);
            }

            var products = await query
                .OrderBy(p => p.ProductId)
                .ToListAsync();

            var dtoList = products.Select(MapToDto).ToList();
            return Ok(dtoList);
        }

        // BƯỚC 1.2 & BÀI TẬP 6.2 (BUỔI 6): API Tìm kiếm kết hợp Phân trang và Lọc danh mục (LINQ Nâng cao)
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,
            [FromQuery] int? categoryId,
            [FromQuery] int? brandId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 5;

            // 1. Khởi tạo câu truy vấn IQueryable (Sử dụng Include để JOIN lấy Category & Brand, AsNoTracking tối ưu RAM)
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsNoTracking()
                .AsQueryable();

            // 2. Tìm kiếm tên sản phẩm gần đúng (.Contains()) hoặc mã vạch (Tiêu chí 24)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(kw) || p.Barcode.ToLower().Contains(kw));
            }

            // 3. Lọc theo nhóm hàng (Category)
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // 4. Lọc theo thương hiệu (Brand)
            if (brandId.HasValue && brandId.Value > 0)
            {
                query = query.Where(p => p.BrandId == brandId.Value);
            }

            // 5. Đếm tổng số bản ghi thỏa mãn điều kiện
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // 6. Thực hiện Phân trang (.Skip() & .Take() - Tiêu chí 25) và chuyển sang DTO (Tiêu chí 15)
            var items = await query
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : "Chưa phân loại",
                    BrandId = p.BrandId,
                    BrandName = p.Brand != null ? p.Brand.BrandName : "Chưa rõ thương hiệu"
                })
                .ToListAsync();

            var response = new PagedResult<ProductItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }

        // 2. READ: Lấy chi tiết một sản phẩm theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }
            return Ok(MapToDto(product));
        }

        // 3. SEARCH: Tìm kiếm sản phẩm theo tên, mã vạch hoặc lọc theo nhóm hàng
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword, [FromQuery] int? categoryId = null)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim();
                query = query.Where(p => p.ProductName.Contains(kw) || p.Barcode.Contains(kw));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var result = await query.OrderBy(p => p.ProductId).ToListAsync();
            return Ok(result.Select(MapToDto).ToList());
        }

        // Lấy chi tiết sản phẩm theo mã vạch
        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Barcode == barcode);

            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }
            return Ok(MapToDto(product));
        }

        // 4. CREATE: Thêm mới sản phẩm (Tiêu chí 30: Chỉ Admin hoặc Warehouse được thêm sản phẩm, Thu ngân chỉ bán hàng)
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(dto.Barcode) || string.IsNullOrWhiteSpace(dto.ProductName))
            {
                return BadRequest(new { message = "Mã vạch và tên sản phẩm không được trống!" });
            }

            if (dto.Price <= 0)
            {
                return BadRequest(new { message = "Giá bán sản phẩm phải lớn hơn 0!" });
            }

            if (await _context.Products.AnyAsync(p => p.Barcode == dto.Barcode.Trim()))
            {
                return BadRequest(new { message = "Mã vạch này đã tồn tại trong hệ thống!" });
            }

            var newProduct = new Product
            {
                Barcode = dto.Barcode.Trim(),
                ProductName = dto.ProductName.Trim(),
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId > 0 ? dto.CategoryId : 1,
                BrandId = dto.BrandId > 0 ? dto.BrandId : 1
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            var created = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == newProduct.ProductId);

            return CreatedAtAction(nameof(GetById), new { id = newProduct.ProductId }, MapToDto(created ?? newProduct));
        }

        // 5. UPDATE: Cập nhật thông tin sản phẩm
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm cần sửa!" });
            }

            if (dto.Price <= 0)
            {
                return BadRequest(new { message = "Giá bán sản phẩm phải lớn hơn 0!" });
            }

            if (await _context.Products.AnyAsync(p => p.Barcode == dto.Barcode.Trim() && p.ProductId != id))
            {
                return BadRequest(new { message = "Mã vạch cập nhật bị trùng với một sản phẩm khác!" });
            }

            product.Barcode = dto.Barcode.Trim();
            product.ProductName = dto.ProductName.Trim();
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            if (dto.CategoryId > 0)
            {
                product.CategoryId = dto.CategoryId;
            }
            if (dto.BrandId > 0)
            {
                product.BrandId = dto.BrandId;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 6. DELETE: Xóa sản phẩm theo ID
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm cần xóa!" });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
