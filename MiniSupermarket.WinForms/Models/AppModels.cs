using System;

namespace MiniSupermarket.WinForms.Models
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
        public string CategoryName => Category?.CategoryName ?? $"Nhóm #{CategoryId}";
        public string Unit { get; set; } = "cái";
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
    }

    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Phone
        {
            get => !string.IsNullOrEmpty(PhoneNumber) ? PhoneNumber : _phone;
            set => _phone = value;
        }
        private string _phone = string.Empty;

        public string MembershipRank { get; set; } = "Chuẩn";
        public string Tier
        {
            get => !string.IsNullOrEmpty(MembershipRank) ? MembershipRank : _tier;
            set => _tier = value;
        }
        private string _tier = "Thành viên";

        public int RewardPoints { get; set; }
    }
}
