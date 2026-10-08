using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders = new();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng!" });
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Order newOrder)
        {
            if (newOrder.Items == null || newOrder.Items.Count == 0)
            {
                return BadRequest(new { message = "Đơn hàng phải có ít nhất 1 mặt hàng!" });
            }

            if (newOrder.CashierId <= 0)
            {
                return BadRequest(new { message = "Vui lòng chọn thu ngân!" });
            }

            newOrder.OrderId = _orders.Count > 0
                ? _orders.Max(o => o.OrderId) + 1
                : 1;

            newOrder.CreatedAt = DateTime.UtcNow;

            decimal subtotal = 0;
            int itemIdCounter = 1;

            foreach (var item in newOrder.Items)
            {
                item.OrderItemId = itemIdCounter++;
                item.OrderId = newOrder.OrderId;

                item.LineTotal = item.Quantity * item.UnitPrice;

                subtotal += item.LineTotal;
            }

            newOrder.Subtotal = subtotal;

            if (newOrder.Discount < 0)
            {
                newOrder.Discount = 0;
            }

            if (newOrder.Discount > newOrder.Subtotal)
            {
                newOrder.Discount = newOrder.Subtotal;
            }

            newOrder.Total = newOrder.Subtotal - newOrder.Discount;

            if (string.IsNullOrWhiteSpace(newOrder.OrderCode))
            {
                newOrder.OrderCode = $"HD{newOrder.OrderId:D5}";
            }

            if (string.IsNullOrWhiteSpace(newOrder.PaymentMethod))
            {
                newOrder.PaymentMethod = "CASH";
            }

            if (string.IsNullOrWhiteSpace(newOrder.Status))
            {
                newOrder.Status = "PAID";
            }

            _orders.Add(newOrder);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newOrder.OrderId },
                newOrder
            );
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng cần cập nhật!" });
            }

            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest(new { message = "Trạng thái không hợp lệ!" });
            }

            order.Status = status;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng cần xóa!" });
            }

            _orders.Remove(order);

            return NoContent();
        }
    }

}