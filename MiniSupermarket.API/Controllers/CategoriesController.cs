using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tiêu chí 29: Phải có JWT Token mới được gọi API
    public class CategoriesController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CategoriesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // 1. READ: Lấy toàn bộ danh mục (Trả về CategoryDto - Tiêu chí 15, 27)
        // GET: /api/categories
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.CategoryId)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(list);
        }

        // =========================================================
        // 2. READ: Lấy chi tiết danh mục theo ID
        // GET: /api/categories/1
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryId == id)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng trong CSDL!"
                });
            }

            return Ok(category);
        }

        // =========================================================
        // 3. SEARCH: Tìm kiếm danh mục bằng LINQ
        // GET: /api/categories/search?keyword=đèn
        // =========================================================
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa tìm kiếm!"
                });
            }

            var result = await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryName.Contains(keyword))
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(result);
        }

        // =========================================================
        // 4. CREATE: Thêm mới danh mục (Tiêu chí 30: Phân quyền Admin)
        // POST: /api/categories
        // =========================================================
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(dto.CategoryName))
            {
                return BadRequest(new
                {
                    message = "Tên nhóm hàng không được để trống!"
                });
            }

            var newCat = new Category
            {
                CategoryName = dto.CategoryName.Trim(),
                Description = dto.Description?.Trim()
            };

            _context.Categories.Add(newCat);
            await _context.SaveChangesAsync();

            dto.CategoryId = newCat.CategoryId;

            return CreatedAtAction(
                nameof(GetById),
                new { id = newCat.CategoryId },
                dto
            );
        }

        // =========================================================
        // 5. UPDATE: Cập nhật danh mục
        // PUT: /api/categories/1
        // =========================================================
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CategoryDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cat = await _context.Categories.FindAsync(id);

            if (cat == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng cần sửa!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateDto.CategoryName))
            {
                return BadRequest(new
                {
                    message = "Tên nhóm hàng không được để trống!"
                });
            }

            cat.CategoryName = updateDto.CategoryName.Trim();
            cat.Description = updateDto.Description?.Trim();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =========================================================
        // 6. DELETE: Xóa danh mục
        // DELETE: /api/categories/1
        // =========================================================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);

            if (cat == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng cần xóa!"
                });
            }

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =========================================================
        // 7. ADMIN DASHBOARD (Chỉ Admin được truy cập - Tiêu chí 30)
        // GET: /api/categories/admin-dashboard
        // =========================================================
        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            int totalCategories = await _context.Categories.AsNoTracking().CountAsync();
            int totalProducts = await _context.Products.AsNoTracking().CountAsync();
            return Ok(new
            {
                message = "Chào mừng Admin! Bạn có toàn quyền quản trị hệ thống siêu thị mini.",
                totalCategories,
                totalProducts
            });
        }

        // =========================================================
        // 8. STAFF POS (Admin và Cashier đều được truy cập - Tiêu chí 30)
        // GET: /api/categories/staff-pos
        // =========================================================
        [HttpGet("staff-pos")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetStaffPos()
        {
            int activeProducts = await _context.Products.AsNoTracking().CountAsync(p => p.StockQuantity > 0);
            return Ok(new
            {
                message = "Màn hình POS Thu ngân sẵn sàng phục vụ bán hàng.",
                activeProducts
            });
        }
    }
}
