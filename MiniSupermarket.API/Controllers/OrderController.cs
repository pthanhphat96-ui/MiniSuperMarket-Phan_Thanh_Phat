using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.Customer)
                .AsNoTracking()
                .ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng!" });
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order newOrder)
        {
            if (newOrder.Items == null || newOrder.Items.Count == 0)
            {
                return BadRequest(new { message = "Đơn hàng phải có ít nhất 1 mặt hàng!" });
            }

            if (newOrder.CashierId <= 0)
            {
                return BadRequest(new { message = "Vui lòng chọn thu ngân!" });
            }

            newOrder.CreatedAt = DateTime.UtcNow;
            
            decimal subtotal = 0;
            foreach (var item in newOrder.Items)
            {
                item.LineTotal = item.Quantity * item.UnitPrice;
                subtotal += item.LineTotal;
            }

            newOrder.Subtotal = subtotal;

            if (newOrder.Discount < 0) newOrder.Discount = 0;
            if (newOrder.Discount > newOrder.Subtotal) newOrder.Discount = newOrder.Subtotal;

            newOrder.Total = newOrder.Subtotal - newOrder.Discount;

            if (string.IsNullOrWhiteSpace(newOrder.PaymentMethod))
                newOrder.PaymentMethod = "CASH";

            if (string.IsNullOrWhiteSpace(newOrder.Status))
                newOrder.Status = "PAID";

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            // Cập nhật lại mã đơn hàng sau khi có OrderId
            if (string.IsNullOrWhiteSpace(newOrder.OrderCode))
            {
                newOrder.OrderCode = $"HD{newOrder.OrderId:D5}";
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = newOrder.OrderId },
                newOrder
            );
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng cần cập nhật!" });
            }

            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest(new { message = "Trạng thái không hợp lệ!" });
            }

            order.Status = status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng cần xóa!" });
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}