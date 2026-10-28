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
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
        }

        private static OrderResponseDto MapOrderToDto(Order o)
        {
            var detailDtos = o.Items.Select(i => new OrderDetailItemDto
            {
                OrderItemId = i.OrderItemId,
                OrderDetailId = i.OrderItemId,
                OrderId = i.OrderId,
                ProductId = i.ProductId,
                ProductName = i.Product != null ? i.Product.ProductName : $"Sản phẩm #{i.ProductId}",
                Barcode = i.Product != null ? i.Product.Barcode : string.Empty,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                LineTotal = i.LineTotal
            }).ToList();

            return new OrderResponseDto
            {
                OrderId = o.OrderId,
                OrderCode = o.OrderCode,
                Subtotal = o.Subtotal,
                Discount = o.Discount,
                Total = o.Total,
                TotalAmount = o.Total,
                FinalAmount = o.FinalAmount > 0 ? o.FinalAmount : o.Total,
                TotalItems = o.TotalItems > 0 ? o.TotalItems : detailDtos.Sum(d => d.Quantity),
                RewardPoints = o.RewardPoints,
                PaymentMethod = o.PaymentMethod,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                CreatedDate = o.CreatedDate,
                OrderDate = o.CreatedAt,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer != null ? o.Customer.CustomerName : "Khách vãng lai",
                CashierId = o.CashierId,
                CashierUsername = !string.IsNullOrWhiteSpace(o.CashierUsername)
                    ? o.CashierUsername
                    : (o.Cashier?.Username ?? $"cashier#{o.CashierId}"),
                CashierFullName = o.Cashier != null ? o.Cashier.FullName : "Thu ngân ca trực",
                Items = detailDtos,
                OrderDetails = detailDtos
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Include(o => o.Customer)
                .Include(o => o.Cashier)
                .AsNoTracking()
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return Ok(orders.Select(MapOrderToDto).ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Include(o => o.Customer)
                .Include(o => o.Cashier)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng!" });
            }

            return Ok(MapOrderToDto(order));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Cashier")]
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
            newOrder.CreatedDate = DateTime.Now;

            decimal subtotal = 0;
            int totalQty = 0;
            foreach (var item in newOrder.Items)
            {
                item.LineTotal = item.Quantity * item.UnitPrice;
                subtotal += item.LineTotal;
                totalQty += item.Quantity;
            }

            newOrder.Subtotal = subtotal;
            newOrder.TotalItems = totalQty;

            if (newOrder.Discount < 0) newOrder.Discount = 0;
            if (newOrder.Discount > newOrder.Subtotal) newOrder.Discount = newOrder.Subtotal;

            newOrder.Total = newOrder.Subtotal - newOrder.Discount;
            newOrder.FinalAmount = newOrder.Total;
            newOrder.RewardPoints = (int)(newOrder.Total / 10000);

            if (string.IsNullOrWhiteSpace(newOrder.PaymentMethod))
                newOrder.PaymentMethod = "CASH";

            if (string.IsNullOrWhiteSpace(newOrder.Status))
                newOrder.Status = "PAID";

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            if (string.IsNullOrWhiteSpace(newOrder.OrderCode))
            {
                newOrder.OrderCode = $"HD{newOrder.OrderId:D5}";
                await _context.SaveChangesAsync();
            }

            var created = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Include(o => o.Customer)
                .Include(o => o.Cashier)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == newOrder.OrderId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newOrder.OrderId },
                MapOrderToDto(created ?? newOrder)
            );
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Cashier")]
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
        [Authorize(Roles = "Admin")]
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
