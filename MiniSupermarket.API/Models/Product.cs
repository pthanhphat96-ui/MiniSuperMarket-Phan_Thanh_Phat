using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Mã vạch sản phẩm không được trống")]
        [StringLength(50, ErrorMessage = "Mã vạch không vượt quá 50 ký tự")]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm không vượt quá 150 ký tự")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giá bán sản phẩm là bắt buộc")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được âm")]
        public int StockQuantity { get; set; }

        // =====================================
        // QUAN HỆ N - 1 VỚI BẢNG CATEGORIES
        // =====================================
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        // =====================================
        // QUAN HỆ N - 1 VỚI BẢNG BRANDS
        // =====================================
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand? Brand { get; set; }

        // =====================================
        // QUAN HỆ 1 - N VỚI BẢNG ORDERITEMS (CHI TIẾT HÓA ĐƠN)
        // =====================================
        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
