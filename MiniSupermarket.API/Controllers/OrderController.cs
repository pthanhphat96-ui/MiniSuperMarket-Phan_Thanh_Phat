using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // Khởi tạo danh sách lưu tạm trên RAM (In-Memory)
        private static readonly List<Order> _orders = new();

        // 1. READ: Lấy toàn bộ danh sách đơn hàng
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orders);
        }

        // 2. READ: Lấy chi tiết đơn hàng (Sẽ bao gồm cả thông tin các sản phẩm nằm trong đơn)
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

        // 3. CREATE: Tạo đơn hàng mới (Kèm theo danh sách sản phẩm)
        [HttpPost]
        public IActionResult Create([FromBody] Order newOrder)
        {
            // Bắt lỗi nếu đơn hàng không có sản phẩm nào
            if (newOrder.OrderDetails == null || newOrder.OrderDetails.Count == 0)
            {
                return BadRequest(new { message = "Đơn hàng phải có ít nhất 1 mặt hàng!" });
            }

            if (newOrder.CustomerId <= 0)
            {
                return BadRequest(new { message = "Vui lòng chọn khách hàng!" });
            }

            // Tự động tăng ID cho Đơn hàng
            newOrder.OrderId = _orders.Count > 0 ? _orders.Max(o => o.OrderId) + 1 : 1;
            newOrder.OrderDate = DateTime.Now;
            newOrder.Status = "Chờ xử lý";

            decimal totalAmount = 0;
            int detailIdCounter = 1;

            // Xử lý từng sản phẩm trong đơn
            foreach (var detail in newOrder.OrderDetails)
            {
                // Gán ID ảo cho Chi tiết đơn
                detail.OrderDetailId = detailIdCounter++;
                detail.OrderId = newOrder.OrderId; // Gắn với ID của Đơn Hàng cha

                // Cộng dồn tính Tổng tiền toàn bộ đơn hàng
                totalAmount += (detail.Quantity * detail.UnitPrice);
            }

            // Gán tổng tiền vào đơn hàng
            newOrder.TotalAmount = totalAmount;

            // Lưu vào list In-Memory
            _orders.Add(newOrder);

            return CreatedAtAction(nameof(GetById), new { id = newOrder.OrderId }, newOrder);
        }

        // 4. UPDATE STATUS: Chỉ cập nhật trạng thái đơn hàng (Duyệt, Đang giao, Đã hủy)
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

            // Đổi trạng thái
            order.Status = status;
            return NoContent();
        }

        // 5. DELETE: Xóa đơn hàng (sẽ tự động xóa bay luôn cả OrderDetails nằm bên trong nó do cấu trúc List in-memory)
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