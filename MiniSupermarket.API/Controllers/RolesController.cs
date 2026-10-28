using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")] // Định tuyến: /api/roles
    [ApiController]
    [Authorize(Roles = "Admin,ADMIN")]
    public class RolesController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public RolesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // 1. READ: Lấy toàn bộ danh sách vai trò (GET /api/roles)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _context.Roles
                .AsNoTracking()
                .OrderBy(r => r.RoleId)
                .Select(r => new RoleDto
                {
                    Id = r.RoleId,
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                    Description = r.RoleName == "ADMIN" ? "Quản trị viên toàn quyền hệ thống"
                                : r.RoleName == "WAREHOUSE" ? "Nhân viên quản lý kho và danh mục"
                                : "Nhân viên thu ngân bán hàng POS"
                })
                .ToListAsync();

            return Ok(roles);
        }

        // 2. READ: Lấy chi tiết một vai trò theo ID (GET /api/roles/{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RoleId == id);

            if (role == null)
            {
                return NotFound(new { message = "Không tìm thấy vai trò!" });
            }

            return Ok(new RoleDto
            {
                Id = role.RoleId,
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = $"Vai trò {role.RoleName}"
            });
        }

        // 3. CREATE: Thêm mới vai trò (POST /api/roles)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RoleName))
            {
                return BadRequest(new { message = "Tên vai trò không được để trống!" });
            }

            var newRole = new Role
            {
                RoleName = dto.RoleName.Trim().ToUpperInvariant()
            };

            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();

            dto.Id = newRole.RoleId;
            dto.RoleId = newRole.RoleId;

            return CreatedAtAction(nameof(GetById), new { id = newRole.RoleId }, dto);
        }

        // 4. UPDATE: Cập nhật thông tin vai trò (PUT /api/roles/{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleDto updateRole)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound(new { message = "Không tìm thấy vai trò cần sửa!" });
            }

            if (string.IsNullOrWhiteSpace(updateRole.RoleName))
            {
                return BadRequest(new { message = "Tên vai trò không được để trống!" });
            }

            role.RoleName = updateRole.RoleName.Trim().ToUpperInvariant();

            _context.Roles.Update(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 5. DELETE: Xóa vai trò theo ID (DELETE /api/roles/{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound(new { message = "Không tìm thấy vai trò cần xóa!" });
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
