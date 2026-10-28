using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải >= 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá bán phải lớn hơn 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }             // Giá tại thời điểm bán

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LineTotal { get; set; }             // Quantity * UnitPrice

        // Quan hệ N-1 với bảng Orders
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]                                       // Tránh vòng lặp Order -> Items -> Order
        public virtual Order? Order { get; set; }

        // Quan hệ N-1 với bảng Products
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
