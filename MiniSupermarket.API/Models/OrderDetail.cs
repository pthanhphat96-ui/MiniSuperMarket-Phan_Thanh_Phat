using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    /// <summary>
    /// Class OrderDetail chuẩn theo Tiêu chí 18 (Ánh xạ chi tiết hóa đơn Order - OrderDetail / OrderItem)
    /// </summary>
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải >= 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá bán phải lớn hơn 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LineTotal { get; set; }

        // Khóa ngoại liên kết 1-N với Order
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public virtual Order? Order { get; set; }

        // Khóa ngoại liên kết 1-N với Product
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
