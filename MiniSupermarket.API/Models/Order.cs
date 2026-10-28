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

        [Required(ErrorMessage = "Mã hóa đơn không được trống")]
        [StringLength(30)]
        public string OrderCode { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }              // Tổng tiền hàng

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }              // Giảm giá nhập tay

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }                 // Khách phải trả

        [Required]
        [StringLength(20)]
        public string PaymentMethod { get; set; } = "CASH";   // CASH, CARD, MOMO, BANK_TRANSFER

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "PAID";          // PAID, CANCELLED, REFUNDED

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Các cột tương thích chuẩn Buổi 5 (Stored Procedure & POS Checkout)
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string CashierUsername { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int TotalItems { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }

        [Range(0, int.MaxValue)]
        public int RewardPoints { get; set; }

        // Quan hệ N-1 với bảng Customers (null = khách vãng lai)
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        // Quan hệ N-1 với bảng Users (Nhân viên thu ngân lập hóa đơn)
        public int CashierId { get; set; }
        [ForeignKey("CashierId")]
        public virtual User? Cashier { get; set; }

        // Quan hệ 1-N với bảng OrderItems (Chi tiết hóa đơn)
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
