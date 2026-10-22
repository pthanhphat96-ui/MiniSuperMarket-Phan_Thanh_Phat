using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // 1. READ: Lấy toàn bộ danh sách sản phẩm kèm thông tin Nhóm hàng (Category)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId = null)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var products = await query
                .OrderBy(p => p.ProductId)
                .ToListAsync();

            return Ok(products);
        }

        // BƯỚC 1.2 & BÀI TẬP 6.2 (BUỔI 6): API Tìm kiếm kết hợp Phân trang và Lọc danh mục (LINQ Nâng cao)
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,
            [FromQuery] int? categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 5;

            // 1. Khởi tạo câu truy vấn IQueryable (Chưa thực thi xuống SQL ngay)
            // Sử dụng Include để JOIN lấy tên Category, AsNoTracking để tối ưu tốc độ đọc
            var query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            // 2. Nếu người dùng có nhập từ khóa tìm kiếm -> Nối thêm mệnh đề WHERE
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(kw) || p.Barcode.Contains(kw));
            }

            // Bài tập 6.2: Nếu có chọn danh mục cần lọc (categoryId > 0) -> Nối thêm mệnh đề WHERE
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // 3. Đếm tổng số bản ghi thỏa mãn điều kiện (Để tính tổng số trang)
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // 4. Thực hiện Phân trang (Skip & Take) và Làm phẳng dữ liệu (Select sang DTO)
            // Lưu ý: Bắt buộc phải có OrderBy trước khi dùng Skip/Take trong EF Core
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
                    // Nếu sản phẩm chưa gắn danh mục, hiển thị "Chưa phân loại"
                    CategoryName = p.Category != null ? p.Category.CategoryName : "Chưa phân loại"
                })
                .ToListAsync(); // Lúc này câu lệnh SQL mới thực sự được gửi xuống Database!

            // 5. Đóng gói kết quả trả về
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
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }
            return Ok(product);
        }

        // 3. SEARCH: Tìm kiếm sản phẩm theo tên, mã vạch hoặc lọc theo nhóm hàng
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword, [FromQuery] int? categoryId = null)
        {
            var query = _context.Products
                .Include(p => p.Category)
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
            return Ok(result);
        }

        // Lấy chi tiết sản phẩm theo mã vạch
        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Barcode == barcode);

            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }
            return Ok(product);
        }

        // 4. CREATE: Thêm mới sản phẩm
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product newProduct)
        {
            if (string.IsNullOrWhiteSpace(newProduct.Barcode) || string.IsNullOrWhiteSpace(newProduct.ProductName))
            {
                return BadRequest(new { message = "Mã vạch và tên sản phẩm không được trống!" });
            }

            if (await _context.Products.AnyAsync(p => p.Barcode == newProduct.Barcode))
            {
                return BadRequest(new { message = "Mã vạch này đã tồn tại trong hệ thống!" });
            }

            if (newProduct.BrandId <= 0)
            {
                newProduct.BrandId = 1; // Mặc định BrandId = 1 nếu không chọn Brand
            }

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newProduct.ProductId }, newProduct);
        }

        // 5. UPDATE: Cập nhật thông tin sản phẩm
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updateProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm cần sửa!" });
            }

            if (await _context.Products.AnyAsync(p => p.Barcode == updateProduct.Barcode && p.ProductId != id))
            {
                return BadRequest(new { message = "Mã vạch cập nhật bị trùng với một sản phẩm khác!" });
            }

            product.Barcode = updateProduct.Barcode;
            product.ProductName = updateProduct.ProductName;
            product.Price = updateProduct.Price;
            product.StockQuantity = updateProduct.StockQuantity;
            product.CategoryId = updateProduct.CategoryId;
            if (updateProduct.BrandId > 0)
            {
                product.BrandId = updateProduct.BrandId;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 6. DELETE: Xóa sản phẩm theo ID
        [HttpDelete("{id}")]
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
