# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)
*Môn học: Lập trình Ứng dụng .NET Core (Mã môn: 229162)*

**Buổi thực hành:** Buổi 4 (Thiết kế Kiến trúc Giao diện WinForms Shell & Điều hướng Phân quyền RBAC)

---

## 🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server - Database)
Dự án tiếp tục sử dụng cơ sở dữ liệu quan hệ thực tế và được nâng cấp mạnh mẽ ở lớp Frontend với kiến trúc giao diện Single-Form hiện đại:

- **Microsoft SQL Server (Database):** Nơi lưu trữ vĩnh viễn dữ liệu của hệ thống. Ở buổi này, CSDL được nạp sẵn bộ dữ liệu kiểm thử phân quyền gồm 15 tài khoản nhân viên (Admin, Cashier, Warehouse).
- **MiniSupermarket.API (Backend):** Dự án ASP.NET Core Web API đóng vai trò trung gian. Hệ thống Controller (`ProductsController`, `OrdersController`, `CustomersController`) đã được tái cấu trúc, loại bỏ dữ liệu ảo (In-Memory) và kết nối trực tiếp vào Entity Framework Core (EF Core) để truy vấn CSDL thật.
- **MiniSupermarket.WinForms (Frontend Client):** Ứng dụng Windows Forms (máy trạm POS/Admin). Áp dụng kiến trúc vỏ bọc trung tâm (`FormMainShell`) phân chia rõ `panelSidebar` và `panelMainContent`. Tích hợp cơ chế tải Form động (`TopLevel = false`, `Dock = Fill`) và hệ thống Role-Based Access Control (RBAC) để tự động thay đổi Menu theo vai trò đăng nhập.

## 🛠️ 2. Công nghệ Sử dụng
- **Ngôn ngữ:** C# (.NET 8.0)
- **Cơ sở dữ liệu:** Microsoft SQL Server
- **ORM:** Entity Framework Core 8.0 (Code-First, Migrations, Data Seeding)
- **Backend:** ASP.NET Core Web API, Async/Await, LINQ to Entities
- **Frontend:** Windows Forms (.NET 8.0), System.Net.Http.Json, **Single-Form Architecture, Role-Based Access Control (RBAC)**
- **Công cụ:** Visual Studio 2022, SQL Server Management Studio (SSMS), Swagger UI

## 📂 3. Cấu trúc Solution Cập nhật (Buổi 4)

```plaintext
MiniSupermarketSystem/
│
├── MiniSupermarket.API/                   # Dự án Web API (Backend)
│   ├── Controllers/ 
│   │   ├── ProductsController.cs          # Nâng cấp kết nối CSDL thật, thêm API quét Barcode
│   │   ├── OrdersController.cs            # Nâng cấp lưu đơn hàng và chi tiết giỏ hàng vào DB
│   │   └── CustomersController.cs         # Thêm API tra cứu khách hàng bằng SĐT
│   ├── Data/                              # Chứa SupermarketDbContext.cs & Data Seeding
│   ├── Migrations/                        # Lịch sử cập nhật Database tự động
│   └── appsettings.json                   # Cấu hình chuỗi kết nối tới SQL Server
│
└── MiniSupermarket.WinForms/              # Dự án Windows Forms (Frontend Client)
    ├── FormLogin.cs                       # Đăng nhập lấy JWT Token & Role người dùng
    ├── FormMainShell.cs                   # [MỚI] Form vỏ bọc trung tâm chứa Sidebar và vùng nhúng động
    ├── FormPOS.cs                         # [MỚI] Màn hình Bán hàng (Quét mã vạch, Tính tiền, Chốt đơn F9)
    ├── FormProductManagement.cs           # [MỚI] Màn hình Quản lý Kho & Sản phẩm
    ├── FormUserManagement.cs              # [MỚI] Quản trị tài khoản và phân quyền
    ├── FormReport.cs                      # [MỚI] Báo cáo doanh thu nhanh cho Admin
    ├── FormCategoryManagement.cs          # Quản lý Nhóm hàng
    ├── FormCustomerManagement.cs          # Quản lý Khách hàng thân thiết
    └── SessionManager.cs                  # Lưu trữ Token và trạng thái Vai trò (Role)

    ## 👨‍💻 5. Tác giả
- **Họ tên sinh viên:** Phan Thanh Phát
- **Mã sinh viên:** 2124110118
- **Lớp học phần:** CCQ2411D
