using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public BrandsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // 1. READ: Lấy toàn bộ danh sách thương hiệu từ CSDL (EF Core + AsNoTracking + DTO)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _context.Brands
                .AsNoTracking()
                .OrderBy(b => b.BrandId)
                .Select(b => new BrandDto
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    Description = b.Description,
                    LogoUrl = b.LogoUrl
                })
                .ToListAsync();

            return Ok(brands);
        }

        // 2. READ: Lấy chi tiết một thương hiệu theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _context.Brands
                .AsNoTracking()
                .Where(b => b.BrandId == id)
                .Select(b => new BrandDto
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    Description = b.Description,
                    LogoUrl = b.LogoUrl
                })
                .FirstOrDefaultAsync();

            if (brand == null)
            {
                return NotFound(new { message = "Không tìm thấy thương hiệu!" });
            }
            return Ok(brand);
        }

        // 3. SEARCH: Tìm kiếm thương hiệu theo tên bằng LINQ
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });
            }

            var result = await _context.Brands
                .AsNoTracking()
                .Where(b => b.BrandName.Contains(keyword))
                .Select(b => new BrandDto
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    Description = b.Description,
                    LogoUrl = b.LogoUrl
                })
                .ToListAsync();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới thương hiệu
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] BrandDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(dto.BrandName))
            {
                return BadRequest(new { message = "Tên thương hiệu không được để trống!" });
            }

            var newBrand = new Brand
            {
                BrandName = dto.BrandName.Trim(),
                Description = dto.Description?.Trim(),
                LogoUrl = dto.LogoUrl?.Trim()
            };

            _context.Brands.Add(newBrand);
            await _context.SaveChangesAsync();

            dto.BrandId = newBrand.BrandId;

            return CreatedAtAction(nameof(GetById), new { id = newBrand.BrandId }, dto);
        }

        // 5. UPDATE: Cập nhật thông tin thương hiệu
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] BrandDto updateBrand)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound(new { message = "Không tìm thấy thương hiệu cần sửa!" });
            }

            if (string.IsNullOrWhiteSpace(updateBrand.BrandName))
            {
                return BadRequest(new { message = "Tên thương hiệu không được để trống!" });
            }

            brand.BrandName = updateBrand.BrandName.Trim();
            brand.Description = updateBrand.Description?.Trim();
            brand.LogoUrl = updateBrand.LogoUrl?.Trim();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. DELETE: Xóa thương hiệu theo ID
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound(new { message = "Không tìm thấy thương hiệu cần xóa!" });
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
