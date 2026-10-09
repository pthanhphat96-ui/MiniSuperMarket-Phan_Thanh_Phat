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

        // 1. READ: Lấy toàn bộ danh sách sản phẩm
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return Ok(products);
        }

        // 2. READ: Lấy chi tiết một sản phẩm theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }
            return Ok(product);
        }

        // 3. SEARCH: Tìm kiếm sản phẩm theo tên hoặc mã vạch
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });
            }

            var result = await _context.Products
                .Where(p => p.ProductName.Contains(keyword) ||
                            p.Barcode.Contains(keyword))
                .AsNoTracking()
                .ToListAsync();

            return Ok(result);
        }

        // Lấy chi tiết sản phẩm theo mã vạch
        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var product = await _context.Products
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