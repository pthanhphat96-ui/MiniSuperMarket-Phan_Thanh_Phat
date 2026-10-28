using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Yêu cầu đăng nhập bằng JWT
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        private static CustomerDto MapToDto(Customer c) => new CustomerDto
        {
            CustomerId = c.CustomerId,
            CustomerName = c.CustomerName,
            PhoneNumber = c.PhoneNumber,
            Address = c.Address,
            RewardPoints = c.RewardPoints,
            MembershipRank = c.MembershipRank
        };

        // 1. GET: Lấy toàn bộ danh sách khách hàng
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .OrderByDescending(c => c.RewardPoints)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();

            return Ok(customers);
        }

        // BÀI TẬP 6.1 (BUỔI 6): API Phân trang danh sách Khách hàng
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedCustomers(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 5;

            var query = _context.Customers
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim();
                query = query.Where(c =>
                    c.CustomerName.Contains(kw) ||
                    c.PhoneNumber.Contains(kw));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .OrderByDescending(c => c.RewardPoints)
                .ThenBy(c => c.CustomerId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();

            var response = new PagedResult<CustomerDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }

        // 2. GET: Lấy chi tiết khách hàng theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng!"
                });
            }

            return Ok(MapToDto(customer));
        }

        // 3. SEARCH: Tìm kiếm theo tên hoặc số điện thoại
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

            var customers = await _context.Customers
                .AsNoTracking()
                .Where(c =>
                    c.CustomerName.Contains(keyword) ||
                    c.PhoneNumber.Contains(keyword))
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();

            return Ok(customers);
        }

        // 4. POST: Thêm mới khách hàng (Admin và Cashier đều được thêm khách hàng tại quầy)
        [HttpPost]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Create([FromBody] CustomerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(dto.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            var phoneExists = await _context.Customers
                .AnyAsync(c => c.PhoneNumber == dto.PhoneNumber.Trim());

            if (phoneExists)
            {
                return Conflict(new
                {
                    message = "Số điện thoại đã được đăng ký!"
                });
            }

            var newCustomer = new Customer
            {
                CustomerName = dto.CustomerName.Trim(),
                PhoneNumber = dto.PhoneNumber.Trim(),
                Address = dto.Address?.Trim(),
                RewardPoints = 0,
                MembershipRank = "Chuẩn"
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = newCustomer.CustomerId },
                MapToDto(newCustomer)
            );
        }

        // 5. PUT: Cập nhật thông tin khách hàng
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CustomerDto updateCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần cập nhật!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            var phoneExists = await _context.Customers
                .AnyAsync(c =>
                    c.PhoneNumber == updateCustomer.PhoneNumber.Trim() &&
                    c.CustomerId != id);

            if (phoneExists)
            {
                return Conflict(new
                {
                    message = "Số điện thoại đã được sử dụng bởi khách hàng khác!"
                });
            }

            customer.CustomerName = updateCustomer.CustomerName.Trim();
            customer.PhoneNumber = updateCustomer.PhoneNumber.Trim();
            customer.Address = updateCustomer.Address?.Trim();
            customer.MembershipRank = string.IsNullOrWhiteSpace(updateCustomer.MembershipRank)
                ? "Chuẩn"
                : updateCustomer.MembershipRank.Trim();
            customer.RewardPoints = updateCustomer.RewardPoints;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 6. DELETE: Xóa khách hàng (Chỉ Admin được xóa khách hàng)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần xóa!"
                });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.PhoneNumber == phone);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng trong CSDL!"
                });
            }

            return Ok(MapToDto(customer));
        }
    }
}
