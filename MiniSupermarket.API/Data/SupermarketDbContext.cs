using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // 1. SEED BRAND - THƯƠNG HIỆU
            // ==========================================
            modelBuilder.Entity<Brand>().HasData(
                new Brand { BrandId = 1, BrandName = "Xiaomi", Description = "Hệ sinh thái nhà thông minh Xiaomi Mijia" },
                new Brand { BrandId = 2, BrandName = "Tuya Smart", Description = "Nền tảng IoT Tuya toàn cầu" },
                new Brand { BrandId = 3, BrandName = "Philips", Description = "Chuyên các thiết bị chiếu sáng thông minh Philips Hue" },
                new Brand { BrandId = 4, BrandName = "Aqara", Description = "Thương hiệu cao cấp thuộc hệ sinh thái Apple HomeKit" },
                new Brand { BrandId = 5, BrandName = "Amazon", Description = "Thiết bị loa và màn hình thông minh Alexa" }
            );

            // ==========================================
            // 2. SEED CATEGORY - DANH MỤC 
            // ==========================================
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Chiếu sáng thông minh", Description = "Bóng đèn thông minh, dây LED, đèn cảm ứng" },
                new Category { CategoryId = 2, CategoryName = "An ninh & Camera", Description = "Camera giám sát trong/ngoài trời, chuông cửa màn hình" },
                new Category { CategoryId = 3, CategoryName = "Khóa cửa thông minh", Description = "Khóa vân tay, khóa nhận diện khuôn mặt, thẻ từ" },
                new Category { CategoryId = 4, CategoryName = "Cảm biến thông minh", Description = "Cảm biến chuyển động, nhiệt độ, cửa, khói" },
                new Category { CategoryId = 5, CategoryName = "Công tắc & Ổ cắm", Description = "Công tắc cảm ứng WiFi/Zigbee, ổ cắm điều khiển từ xa" },
                new Category { CategoryId = 6, CategoryName = "Loa & Trợ lý ảo", Description = "Loa Google Nest, Amazon Echo, Apple HomePod" },
                new Category { CategoryId = 7, CategoryName = "Nhà bếp thông minh", Description = "Nồi chiên không dầu tự động, máy pha cà phê thông minh" },
                new Category { CategoryId = 8, CategoryName = "Robot hút bụi & Lau nhà", Description = "Robot dọn dẹp tự động, máy hút bụi cầm tay" },
                new Category { CategoryId = 9, CategoryName = "Xử lý không khí", Description = "Máy lọc không khí, máy tạo ẩm, máy hút ẩm thông minh" },
                new Category { CategoryId = 10, CategoryName = "Điều khiển trung tâm (Hub)", Description = "Bộ điều khiển trung tâm Aqara, Tuya, Xiaomi" },
                new Category { CategoryId = 11, CategoryName = "Động cơ & Rèm cửa", Description = "Động cơ rèm cuốn, rèm vải thông minh tự động" },
                new Category { CategoryId = 12, CategoryName = "Thiết bị vệ sinh thông minh", Description = "Nắp bồn cầu tự động, máy sấy tay, gương thông minh" },
                new Category { CategoryId = 13, CategoryName = "Chăm sóc sức khỏe", Description = "Cân sức khỏe thông minh, máy đo huyết áp kết nối app" },
                new Category { CategoryId = 14, CategoryName = "Sân vườn tự động", Description = "Van tưới cây tự động, máy cắt cỏ robot" },
                new Category { CategoryId = 15, CategoryName = "Phụ kiện Smart Home", Description = "Pin, dây cáp, remote, bộ chuyển đổi tín hiệu" }
            );

            // ==========================================
            // 3. SEED CUSTOMER - KHÁCH HÀNG (Đầy đủ 15 người)
            // ==========================================
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Nguyễn Văn A", PhoneNumber = "0901122334", Address = "25 Nguyễn Huệ, Quận 1, TP.HCM", RewardPoints = 1500, MembershipRank = "Vàng" },
                new Customer { CustomerId = 2, CustomerName = "Trần Thị B", PhoneNumber = "0918877665", Address = "118 Võ Văn Tần, Quận 3, TP.HCM", RewardPoints = 500, MembershipRank = "Bạc" },
                new Customer { CustomerId = 3, CustomerName = "Lê Văn C", PhoneNumber = "0983344556", Address = "72 Nguyễn Trãi, Quận 5, TP.HCM", RewardPoints = 100, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 4, CustomerName = "Phạm Thị D", PhoneNumber = "0905678123", Address = "156 Thành Thái, Quận 10, TP.HCM", RewardPoints = 2300, MembershipRank = "Vàng" },
                new Customer { CustomerId = 5, CustomerName = "Hoàng Văn E", PhoneNumber = "0912345678", Address = "43 Điện Biên Phủ, Quận Bình Thạnh, TP.HCM", RewardPoints = 800, MembershipRank = "Bạc" },
                new Customer { CustomerId = 6, CustomerName = "Võ Thị F", PhoneNumber = "0987654321", Address = "89 Phạm Văn Đồng, Quận Gò Vấp, TP.HCM", RewardPoints = 3200, MembershipRank = "Kim Cương" },
                new Customer { CustomerId = 7, CustomerName = "Đặng Văn G", PhoneNumber = "0909876543", Address = "215 Cộng Hòa, Quận Tân Bình, TP.HCM", RewardPoints = 250, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 8, CustomerName = "Bùi Thị H", PhoneNumber = "0913456789", Address = "36 Phan Đình Phùng, Quận Phú Nhuận, TP.HCM", RewardPoints = 1200, MembershipRank = "Bạc" },
                new Customer { CustomerId = 9, CustomerName = "Đỗ Văn I", PhoneNumber = "0981234567", Address = "102 Nguyễn Thị Thập, Quận 7, TP.HCM", RewardPoints = 4500, MembershipRank = "Kim Cương" },
                new Customer { CustomerId = 10, CustomerName = "Nguyễn Thị K", PhoneNumber = "0903456789", Address = "68 Hậu Giang, Quận 6, TP.HCM", RewardPoints = 150, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 11, CustomerName = "Trương Văn L", PhoneNumber = "0915678901", Address = "145 Võ Văn Ngân, TP. Thủ Đức, TP.HCM", RewardPoints = 1800, MembershipRank = "Vàng" },
                new Customer { CustomerId = 12, CustomerName = "Phan Thị M", PhoneNumber = "0986789012", Address = "234 Lê Văn Khương, Quận 12, TP.HCM", RewardPoints = 700, MembershipRank = "Bạc" },
                new Customer { CustomerId = 13, CustomerName = "Lý Văn N", PhoneNumber = "0907890123", Address = "57 Lũy Bán Bích, Quận Tân Phú, TP.HCM", RewardPoints = 5000, MembershipRank = "Kim Cương" },
                new Customer { CustomerId = 14, CustomerName = "Huỳnh Thị P", PhoneNumber = "0918901234", Address = "321 Tỉnh Lộ 10, Quận Bình Tân, TP.HCM", RewardPoints = 350, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 15, CustomerName = "Mai Văn Q", PhoneNumber = "0989012345", Address = "78 Nguyễn Hữu Trí, Huyện Bình Chánh, TP.HCM", RewardPoints = 2600, MembershipRank = "Vàng" }
            );

            // ==========================================
            // 4. SEED PRODUCT - SẢN PHẨM 
            // ==========================================
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 16, Barcode = "SMH000016", ProductName = "Dây LED thông minh RGB trang trí phòng", Price = 450000m, StockQuantity = 100, CategoryId = 1, BrandId = 3 },
                new Product { ProductId = 17, Barcode = "SMH000017", ProductName = "Chuông cửa màn hình thông minh Ring", Price = 2500000m, StockQuantity = 30, CategoryId = 2, BrandId = 5 },
                new Product { ProductId = 18, Barcode = "SMH000018", ProductName = "Khóa cửa thẻ từ khách sạn Tuya", Price = 1800000m, StockQuantity = 50, CategoryId = 3, BrandId = 2 },
                new Product { ProductId = 19, Barcode = "SMH000019", ProductName = "Cảm biến báo khói báo cháy thông minh", Price = 400000m, StockQuantity = 70, CategoryId = 4, BrandId = 1 },
                new Product { ProductId = 20, Barcode = "SMH000020", ProductName = "Công tắc cảm ứng âm tường mặt kính 3 nút", Price = 250000m, StockQuantity = 150, CategoryId = 5, BrandId = 2 },
                new Product { ProductId = 21, Barcode = "SMH000021", ProductName = "Màn hình thông minh Amazon Echo Show 8", Price = 2100000m, StockQuantity = 40, CategoryId = 6, BrandId = 5 },
                new Product { ProductId = 22, Barcode = "SMH000022", ProductName = "Máy pha cà phê thông minh kết nối WiFi", Price = 3500000m, StockQuantity = 20, CategoryId = 7, BrandId = 1 },
                new Product { ProductId = 23, Barcode = "SMH000023", ProductName = "Máy hút bụi cầm tay không dây Dyson", Price = 12000000m, StockQuantity = 10, CategoryId = 8, BrandId = 1 },
                new Product { ProductId = 24, Barcode = "SMH000024", ProductName = "Máy tạo độ ẩm thông minh Deerma", Price = 550000m, StockQuantity = 85, CategoryId = 9, BrandId = 1 },
                new Product { ProductId = 25, Barcode = "SMH000025", ProductName = "Bộ điều khiển trung tâm Zigbee 3.0", Price = 650000m, StockQuantity = 100, CategoryId = 10, BrandId = 4 },
                new Product { ProductId = 26, Barcode = "SMH000026", ProductName = "Động cơ kéo rèm vải thông minh Xiaomi", Price = 1850000m, StockQuantity = 25, CategoryId = 11, BrandId = 1 },
                new Product { ProductId = 27, Barcode = "SMH000027", ProductName = "Gương thông minh tích hợp đèn LED phòng tắm", Price = 2200000m, StockQuantity = 15, CategoryId = 12, BrandId = 2 },
                new Product { ProductId = 28, Barcode = "SMH000028", ProductName = "Máy đo huyết áp bắp tay Bluetooth Omron", Price = 1450000m, StockQuantity = 60, CategoryId = 13, BrandId = 1 },
                new Product { ProductId = 29, Barcode = "SMH000029", ProductName = "Robot cắt cỏ tự động ngoài trời", Price = 18000000m, StockQuantity = 5, CategoryId = 14, BrandId = 2 },
                new Product { ProductId = 30, Barcode = "SMH000030", ProductName = "Cảm biến nhiệt độ và độ ẩm phòng", Price = 120000m, StockQuantity = 200, CategoryId = 15, BrandId = 4 }
            );

            // ==========================================
            // 5. SEED ORDER - ĐƠN HÀNG (5 Đơn)
            // ==========================================
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, CustomerId = 1, OrderDate = new DateTime(2023, 10, 1, 14, 30, 0), TotalAmount = 2950000m, Status = "Đã giao hàng", ShippingAddress = "25 Nguyễn Huệ, Quận 1, TP.HCM" },
                new Order { OrderId = 2, CustomerId = 2, OrderDate = new DateTime(2023, 10, 5, 9, 15, 0), TotalAmount = 2300000m, Status = "Đang xử lý", ShippingAddress = "118 Võ Văn Tần, Quận 3, TP.HCM" },
                new Order { OrderId = 3, CustomerId = 6, OrderDate = new DateTime(2023, 10, 10, 11, 0, 0), TotalAmount = 14100000m, Status = "Đã giao hàng", ShippingAddress = "89 Phạm Văn Đồng, Quận Gò Vấp, TP.HCM" },
                new Order { OrderId = 4, CustomerId = 9, OrderDate = new DateTime(2023, 10, 12, 16, 45, 0), TotalAmount = 650000m, Status = "Đã hủy", ShippingAddress = "102 Nguyễn Thị Thập, Quận 7, TP.HCM" },
                new Order { OrderId = 5, CustomerId = 13, OrderDate = new DateTime(2023, 10, 15, 8, 20, 0), TotalAmount = 4300000m, Status = "Đang giao hàng", ShippingAddress = "57 Lũy Bán Bích, Quận Tân Phú, TP.HCM" }
            );

            // ==========================================
            // 6. SEED ORDER DETAIL - CHI TIẾT ĐƠN HÀNG
            // ==========================================
            modelBuilder.Entity<OrderDetail>().HasData(
                // Đơn hàng 1 (Total: 2,950,000)
                new OrderDetail { OrderDetailId = 1, OrderId = 1, ProductId = 16, Quantity = 1, UnitPrice = 450000m },
                new OrderDetail { OrderDetailId = 2, OrderId = 1, ProductId = 17, Quantity = 1, UnitPrice = 2500000m },

                // Đơn hàng 2 (Total: 2,300,000)
                new OrderDetail { OrderDetailId = 3, OrderId = 2, ProductId = 18, Quantity = 1, UnitPrice = 1800000m },
                new OrderDetail { OrderDetailId = 4, OrderId = 2, ProductId = 20, Quantity = 2, UnitPrice = 250000m },

                // Đơn hàng 3 (Total: 14,100,000) - Đại gia mua máy hút bụi xịn
                new OrderDetail { OrderDetailId = 5, OrderId = 3, ProductId = 23, Quantity = 1, UnitPrice = 12000000m },
                new OrderDetail { OrderDetailId = 6, OrderId = 3, ProductId = 21, Quantity = 1, UnitPrice = 2100000m },

                // Đơn hàng 4 (Total: 650,000) - Đã hủy
                new OrderDetail { OrderDetailId = 7, OrderId = 4, ProductId = 25, Quantity = 1, UnitPrice = 650000m },

                // Đơn hàng 5 (Total: 4,300,000)
                new OrderDetail { OrderDetailId = 8, OrderId = 5, ProductId = 26, Quantity = 1, UnitPrice = 1850000m },
                new OrderDetail { OrderDetailId = 9, OrderId = 5, ProductId = 27, Quantity = 1, UnitPrice = 2200000m },
                new OrderDetail { OrderDetailId = 10, OrderId = 5, ProductId = 20, Quantity = 1, UnitPrice = 250000m }
            );
        }
    }
}