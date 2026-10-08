using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Khởi tạo danh sách trống, chỉ đóng vai trò nơi lưu trữ tạm thời
        private static readonly List<Product> _products = new();

        // 1. READ: Lấy toàn bộ danh sách sản phẩm
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_products);
        }

        // 2. READ: Lấy chi tiết một sản phẩm theo ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }
            return Ok(product);
        }

        // 3. SEARCH: Tìm kiếm sản phẩm theo tên hoặc mã vạch
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });
            }

            var result = _products
                .Where(p => p.ProductName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            p.Barcode.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới sản phẩm
        [HttpPost]
        public IActionResult Create([FromBody] Product newProduct)
        {
            if (string.IsNullOrWhiteSpace(newProduct.Barcode) || string.IsNullOrWhiteSpace(newProduct.ProductName))
            {
                return BadRequest(new { message = "Mã vạch và tên sản phẩm không được trống!" });
            }

            if (_products.Any(p => p.Barcode == newProduct.Barcode))
            {
                return BadRequest(new { message = "Mã vạch này đã tồn tại trong hệ thống!" });
            }

            newProduct.ProductId = _products.Count > 0 ? _products.Max(p => p.ProductId) + 1 : 1;
            _products.Add(newProduct);

            return CreatedAtAction(nameof(GetById), new { id = newProduct.ProductId }, newProduct);
        }

        // 5. UPDATE: Cập nhật thông tin sản phẩm
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product updateProduct)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm cần sửa!" });
            }

            if (_products.Any(p => p.Barcode == updateProduct.Barcode && p.ProductId != id))
            {
                return BadRequest(new { message = "Mã vạch cập nhật bị trùng với một sản phẩm khác!" });
            }

            product.Barcode = updateProduct.Barcode;
            product.ProductName = updateProduct.ProductName;
            product.Price = updateProduct.Price;
            product.StockQuantity = updateProduct.StockQuantity;
            product.CategoryId = updateProduct.CategoryId;

            return NoContent();
        }

        // 6. DELETE: Xóa sản phẩm theo ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm cần xóa!" });
            }

            _products.Remove(product);
            return NoContent();
        }
    }
}