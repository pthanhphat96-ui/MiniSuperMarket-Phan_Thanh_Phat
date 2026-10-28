using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    [Table("Customers")]
    public class Customer
    {
        // Khóa chính - tự tăng IDENTITY(1,1)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        // Tên khách hàng - bắt buộc, NVARCHAR(100)
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 ký tự")]
        public string CustomerName { get; set; } = string.Empty;

        // Số điện thoại - bắt buộc, VARCHAR(15)
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Địa chỉ - có thể để trống, NVARCHAR(200)
        [StringLength(200)]
        public string? Address { get; set; }

        // Điểm tích lũy - mặc định 0
        [Range(0, int.MaxValue, ErrorMessage = "Điểm tích lũy không được âm")]
        public int RewardPoints { get; set; } = 0;

        // Hạng thành viên - mặc định "Chuẩn"
        [StringLength(50)]
        public string MembershipRank { get; set; } = "Chuẩn";

        // Quan hệ 1-N: 1 Khách hàng có nhiều Hóa đơn
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
