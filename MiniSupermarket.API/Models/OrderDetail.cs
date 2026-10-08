using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        [Required]
        public int Quantity { get; set; } // Số lượng mua

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } // Đơn giá tại thời điểm mua (để lỡ sau này sản phẩm đổi giá thì hóa đơn cũ không bị đổi theo)

        // =====================================
        // QUAN HỆ VỚI BẢNG ORDERS (N Chi tiết - 1 Đơn hàng)
        // =====================================
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        // =====================================
        // QUAN HỆ VỚI BẢNG PRODUCTS (N Chi tiết - 1 Sản phẩm)
        // =====================================
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}