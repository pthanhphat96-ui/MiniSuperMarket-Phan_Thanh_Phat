using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        // Ngày giờ đặt hàng, tự động lấy thời gian hiện tại
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Tổng tiền của toàn bộ đơn hàng
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Trạng thái đơn hàng (VD: Chờ xử lý, Đang giao, Đã hoàn thành, Đã hủy)
        [StringLength(50)]
        public string Status { get; set; } = "Chờ xử lý";

        [StringLength(255)]
        public string? ShippingAddress { get; set; } // Địa chỉ giao hàng (nếu có)

        // =====================================
        // QUAN HỆ VỚI BẢNG CUSTOMERS (1 Khách hàng - N Đơn hàng)
        // =====================================
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        // =====================================
        // QUAN HỆ 1-N VỚI BẢNG ORDER DETAILS
        // =====================================
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}