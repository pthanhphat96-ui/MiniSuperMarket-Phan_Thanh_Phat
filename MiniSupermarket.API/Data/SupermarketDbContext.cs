using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    // DbContext đại diện cho phiên làm việc với cơ sở dữ liệu SQL Server
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(
            DbContextOptions<SupermarketDbContext> options
        ) : base(options)
        {
        }

        // ==========================================
        // KHAI BÁO CÁC BẢNG DỮ LIỆU
        // ==========================================

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }


        // ==========================================
        // CẤU HÌNH DỮ LIỆU MỒI (SMART HOME)
        // ==========================================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // SEED 15 CATEGORY - DANH MỤC SMART HOME
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
            // SEED 15 CUSTOMER
            // ==========================================

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Nguyễn Văn A", PhoneNumber = "0901122334", Address = "Quận 1, TP.HCM", RewardPoints = 1500, MembershipRank = "Vàng" },
                new Customer { CustomerId = 2, CustomerName = "Trần Thị B", PhoneNumber = "0918877665", Address = "Quận 3, TP.HCM", RewardPoints = 500, MembershipRank = "Bạc" },
                new Customer { CustomerId = 3, CustomerName = "Lê Văn C", PhoneNumber = "0983344556", Address = "Quận 5, TP.HCM", RewardPoints = 100, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 4, CustomerName = "Phạm Thị D", PhoneNumber = "0905678123", Address = "Quận 10, TP.HCM", RewardPoints = 2300, MembershipRank = "Vàng" },
                new Customer { CustomerId = 5, CustomerName = "Hoàng Văn E", PhoneNumber = "0912345678", Address = "Quận Bình Thạnh, TP.HCM", RewardPoints = 800, MembershipRank = "Bạc" },
                new Customer { CustomerId = 6, CustomerName = "Võ Thị F", PhoneNumber = "0987654321", Address = "Quận Gò Vấp, TP.HCM", RewardPoints = 3200, MembershipRank = "Kim Cương" },
                new Customer { CustomerId = 7, CustomerName = "Đặng Văn G", PhoneNumber = "0909876543", Address = "Quận Tân Bình, TP.HCM", RewardPoints = 250, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 8, CustomerName = "Bùi Thị H", PhoneNumber = "0913456789", Address = "Quận Phú Nhuận, TP.HCM", RewardPoints = 1200, MembershipRank = "Bạc" },
                new Customer { CustomerId = 9, CustomerName = "Đỗ Văn I", PhoneNumber = "0981234567", Address = "Quận 7, TP.HCM", RewardPoints = 4500, MembershipRank = "Kim Cương" },
                new Customer { CustomerId = 10, CustomerName = "Nguyễn Thị K", PhoneNumber = "0903456789", Address = "Quận 6, TP.HCM", RewardPoints = 150, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 11, CustomerName = "Trương Văn L", PhoneNumber = "0915678901", Address = "TP. Thủ Đức, TP.HCM", RewardPoints = 1800, MembershipRank = "Vàng" },
                new Customer { CustomerId = 12, CustomerName = "Phan Thị M", PhoneNumber = "0986789012", Address = "Quận 12, TP.HCM", RewardPoints = 700, MembershipRank = "Bạc" },
                new Customer { CustomerId = 13, CustomerName = "Lý Văn N", PhoneNumber = "0907890123", Address = "Quận Tân Phú, TP.HCM", RewardPoints = 5000, MembershipRank = "Kim Cương" },
                new Customer { CustomerId = 14, CustomerName = "Huỳnh Thị P", PhoneNumber = "0918901234", Address = "Quận Bình Tân, TP.HCM", RewardPoints = 350, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 15, CustomerName = "Mai Văn Q", PhoneNumber = "0989012345", Address = "Huyện Bình Chánh, TP.HCM", RewardPoints = 2600, MembershipRank = "Vàng" }
            );


            // ==========================================
            // SEED 15 PRODUCT - SẢN PHẨM SMART HOME
            // ==========================================

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Barcode = "SMH000001", ProductName = "Bóng đèn thông minh Philips Hue Color", Price = 1290000m, StockQuantity = 50, CategoryId = 1 },
                new Product { ProductId = 2, Barcode = "SMH000002", ProductName = "Camera WiFi xoay 360 Ezviz C6N", Price = 550000m, StockQuantity = 120, CategoryId = 2 },
                new Product { ProductId = 3, Barcode = "SMH000003", ProductName = "Khóa cửa vân tay thông minh Xiaomi", Price = 4500000m, StockQuantity = 20, CategoryId = 3 },
                new Product { ProductId = 4, Barcode = "SMH000004", ProductName = "Cảm biến chuyển động gắn tường Aqara", Price = 350000m, StockQuantity = 80, CategoryId = 4 },
                new Product { ProductId = 5, Barcode = "SMH000005", ProductName = "Ổ cắm điện WiFi đo công suất Tuya", Price = 150000m, StockQuantity = 200, CategoryId = 5 },
                new Product { ProductId = 6, Barcode = "SMH000006", ProductName = "Loa trợ lý ảo Google Nest Mini", Price = 690000m, StockQuantity = 60, CategoryId = 6 },
                new Product { ProductId = 7, Barcode = "SMH000007", ProductName = "Nồi chiên không dầu Xiaomi Smart Air Fryer", Price = 1490000m, StockQuantity = 40, CategoryId = 7 },
                new Product { ProductId = 8, Barcode = "SMH000008", ProductName = "Robot hút bụi Roborock S8 Pro Ultra", Price = 24990000m, StockQuantity = 15, CategoryId = 8 },
                new Product { ProductId = 9, Barcode = "SMH000009", ProductName = "Máy lọc không khí Xiaomi Mi Air Purifier 4", Price = 3290000m, StockQuantity = 30, CategoryId = 9 },
                new Product { ProductId = 10, Barcode = "SMH000010", ProductName = "Bộ điều khiển trung tâm Aqara Hub M2", Price = 1190000m, StockQuantity = 45, CategoryId = 10 },
                new Product { ProductId = 11, Barcode = "SMH000011", ProductName = "Động cơ rèm cuốn tự động Tuya WiFi", Price = 1250000m, StockQuantity = 25, CategoryId = 11 },
                new Product { ProductId = 12, Barcode = "SMH000012", ProductName = "Nắp bồn cầu sưởi ấm thông minh TOTO", Price = 8500000m, StockQuantity = 10, CategoryId = 12 },
                new Product { ProductId = 13, Barcode = "SMH000013", ProductName = "Cân sức khỏe thông minh Xiaomi Body Composition", Price = 390000m, StockQuantity = 100, CategoryId = 13 },
                new Product { ProductId = 14, Barcode = "SMH000014", ProductName = "Van nước tưới cây tự động WiFi", Price = 850000m, StockQuantity = 35, CategoryId = 14 },
                new Product { ProductId = 15, Barcode = "SMH000015", ProductName = "Bộ Hub hồng ngoại điều khiển TV/Điều hòa", Price = 180000m, StockQuantity = 150, CategoryId = 15 }
            );
        }
    }
}