using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.Models
{
    // =========================================================================
    // 1. DTO SẢN PHẨM (ProductItemDto & ProductRequestDto)
    // =========================================================================
    public class ProductItemDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int? BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public CategoryDto? Category { get; set; }
        public BrandDto? Brand { get; set; }
    }

    public class ProductRequestDto
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Mã vạch không được để trống!")]
        [StringLength(50)]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        [StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0!")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Tồn kho không được âm!")]
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public int BrandId { get; set; } = 1;
    }

    // =========================================================================
    // 2. DTO DANH MỤC NHÓM HÀNG (CategoryDto)
    // =========================================================================
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên nhóm hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhóm hàng không vượt quá 100 ký tự")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }
    }

    // =========================================================================
    // 3. DTO THƯƠNG HIỆU (BrandDto)
    // =========================================================================
    public class BrandDto
    {
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Tên thương hiệu không được để trống!")]
        [StringLength(100)]
        public string BrandName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? LogoUrl { get; set; }
    }

    // =========================================================================
    // 4. DTO KHÁCH HÀNG (CustomerDto)
    // =========================================================================
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Address { get; set; }

        [Range(0, int.MaxValue)]
        public int RewardPoints { get; set; } = 0;

        [StringLength(50)]
        public string MembershipRank { get; set; } = "Chuẩn";
    }

    // =========================================================================
    // 5. DTO VAI TRÒ (RoleDto)
    // =========================================================================
    public class RoleDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Tên vai trò không được để trống!")]
        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }

    // =========================================================================
    // 6. DTO ĐƠN HÀNG & CHI TIẾT ĐƠN HÀNG (OrderResponseDto & OrderDetailItemDto)
    // =========================================================================
    public class OrderDetailItemDto
    {
        public int OrderItemId { get; set; }
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public int TotalItems { get; set; }
        public int RewardPoints { get; set; }
        public string PaymentMethod { get; set; } = "CASH";
        public string Status { get; set; } = "PAID";
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; } = "Khách vãng lai";
        public int CashierId { get; set; }
        public string CashierUsername { get; set; } = string.Empty;
        public string CashierFullName { get; set; } = string.Empty;
        public List<OrderDetailItemDto> Items { get; set; } = new();
        public List<OrderDetailItemDto> OrderDetails { get; set; } = new();
    }

    // =========================================================================
    // 7. KHUÔN MẪU (GENERIC CLASS) DÙNG CHUNG CHO MỌI API CÓ PHÂN TRANG
    // =========================================================================
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
