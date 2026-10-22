using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,ADMIN")]
    public class UsersController : ControllerBase
    {
        private readonly SupermarketDbContext _db;

        public UsersController(SupermarketDbContext db)
        {
            _db = db;
        }

        private static string NormalizeRoleName(string? roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName)) return "Cashier";
            return roleName.Trim().ToUpperInvariant() switch
            {
                "ADMIN" => "Admin",
                "WAREHOUSE" => "Warehouse",
                _ => "Cashier"
            };
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var q = _db.Users.Include(u => u.Role).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim();
                q = q.Where(u => u.Username.Contains(kw) || u.FullName.Contains(kw) || (u.Email != null && u.Email.Contains(kw)));
            }

            var rawList = await q.OrderBy(u => u.UserId).ToListAsync();
            var list = rawList.Select(u => new
            {
                Id = u.UserId,
                u.UserId,
                u.Username,
                u.FullName,
                Email = u.Email ?? string.Empty,
                Phone = u.Phone ?? string.Empty,
                u.IsActive,
                u.RoleId,
                Role = NormalizeRoleName(u.Role?.RoleName),
                u.CreatedAt
            }).ToList();

            return Ok(list);
        }

        // GET: api/Users/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var u = await _db.Users.Include(x => x.Role).AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
            if (u == null) return NotFound(new { message = "Không tìm thấy người dùng" });

            return Ok(new
            {
                Id = u.UserId,
                u.UserId,
                u.Username,
                u.FullName,
                Email = u.Email ?? string.Empty,
                Phone = u.Phone ?? string.Empty,
                u.IsActive,
                u.RoleId,
                Role = NormalizeRoleName(u.Role?.RoleName),
                u.CreatedAt
            });
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { message = "Tên đăng nhập và mật khẩu không được để trống" });

            if (await _db.Users.AnyAsync(u => u.Username == dto.Username.Trim()))
                return Conflict(new { message = "Tên đăng nhập đã tồn tại" });

            var targetRoleName = (dto.Role ?? "CASHIER").Trim().ToUpperInvariant();
            var role = await _db.Roles.FirstOrDefaultAsync(r => r.RoleName.ToUpper() == targetRoleName)
                       ?? await _db.Roles.FirstOrDefaultAsync(r => r.RoleName.ToUpper() == "CASHIER");

            var user = new User
            {
                Username = dto.Username.Trim(),
                PasswordHash = HashPassword(dto.Password.Trim()),
                FullName = string.IsNullOrWhiteSpace(dto.FullName) ? dto.Username.Trim() : dto.FullName.Trim(),
                Email = dto.Email?.Trim(),
                Phone = dto.Phone?.Trim(),
                IsActive = dto.IsActive,
                RoleId = role != null ? role.RoleId : 3,
                CreatedAt = DateTime.UtcNow
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, new
            {
                Id = user.UserId,
                user.UserId,
                user.Username,
                user.FullName,
                Email = user.Email ?? string.Empty,
                Phone = user.Phone ?? string.Empty,
                user.IsActive,
                user.RoleId,
                Role = NormalizeRoleName(role?.RoleName),
                user.CreatedAt
            });
        }

        // PUT: api/Users/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy người dùng" });

            if (!string.IsNullOrWhiteSpace(dto.FullName))
                user.FullName = dto.FullName.Trim();

            user.Email = dto.Email?.Trim();
            user.Phone = dto.Phone?.Trim();
            user.IsActive = dto.IsActive;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = HashPassword(dto.Password.Trim());
            }

            if (!string.IsNullOrWhiteSpace(dto.Role))
            {
                string roleUpper = dto.Role.Trim().ToUpperInvariant();
                var role = await _db.Roles.FirstOrDefaultAsync(r => r.RoleName.ToUpper() == roleUpper);
                if (role != null) user.RoleId = role.RoleId;
            }

            await _db.SaveChangesAsync();
            return Ok(new { message = "Cập nhật thông tin tài khoản thành công!" });
        }

        // PUT: api/Users/5/toggle-status
        [HttpPut("{id:int}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id, [FromBody] ToggleStatusDto? dto)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản cần khóa/mở khóa!" });

            bool newStatus = dto?.IsActive ?? !user.IsActive;
            user.IsActive = newStatus;

            await _db.SaveChangesAsync();

            return Ok(new
            {
                Id = user.UserId,
                user.UserId,
                user.Username,
                user.IsActive,
                message = user.IsActive
                    ? $"Đã mở khóa tài khoản '{user.Username}' thành công!"
                    : $"Đã khóa tài khoản '{user.Username}' thành công!"
            });
        }

        // PUT: api/Users/5/reset-password
        [HttpPut("{id:int}/reset-password")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { message = "Mật khẩu mới không được để trống!" });
            }

            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản!" });

            user.PasswordHash = HashPassword(dto.Password.Trim());
            await _db.SaveChangesAsync();

            return Ok(new { message = $"Đã đặt lại mật khẩu cho tài khoản '{user.Username}' thành công!" });
        }

        public static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy người dùng" });

            if (user.Username.Equals("admin", StringComparison.OrdinalIgnoreCase) ||
                user.Username.Equals("admin01", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { message = "Không thể xóa tài khoản quản trị tối cao" });

            if (await _db.Orders.AnyAsync(o => o.CashierId == id))
            {
                user.IsActive = false;
                await _db.SaveChangesAsync();
                return Ok(new { message = "Người dùng đã có hóa đơn trong hệ thống, đã chuyển sang trạng thái khóa tài khoản" });
            }

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }

    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; } = "CASHIER";
        public bool IsActive { get; set; } = true;
    }

    public class UpdateUserDto
    {
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class ToggleStatusDto
    {
        public bool? IsActive { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Password { get; set; } = string.Empty;
    }
}
