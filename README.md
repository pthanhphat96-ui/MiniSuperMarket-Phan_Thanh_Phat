PHẦN 1: HƯỚNG DẪN NHANH ĐẨY CODE LÊN GITHUB (Dành cho sinh viên)
Mở Git Bash (hoặc Terminal trong Visual Studio) tại thư mục chứa Solution của bạn và chạy lần lượt các lệnh sau:

Khởi tạo kho lưu trữ Git cục bộ:
git init
Thêm toàn bộ tệp tin vào vùng chờ:
git add .
Lưu lịch sử thay đổi (Commit):
git commit -m "Hoàn thành bài thực hành Buổi 1: CRUD Nhóm hàng MiniSupermarket"
Đổi tên nhánh chính thành main:
git branch -M main
Liên kết với kho lưu trữ trên GitHub của bạn (thay URL bằng link repo của bạn):
git remote add origin https://github.com/TEN_TAI_KHOAN/MiniSupermarket-Session1.git
Đẩy code lên GitHub:
git push -u origin main

PHẦN 2: MẪU TỆP README.md ĐỂ TẠO TRANG CHỦ GITHUB REPOSITORY
Bạn hãy tạo một tệp tên là README.md ở thư mục gốc của Solution và dán nội dung sau vào:

Markdown
# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)
> **Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)  
> **Buổi thực hành:** Buổi 1 - Xây dựng Web API quản lý danh mục và kết nối WinForms Client (CRUD)

---

## 🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server)
Dự án được xây dựng theo mô hình phân tầng hiện đại, tách biệt hoàn toàn giữa Backend và Frontend:
* **`MiniSupermarket.API` (Backend):** Dự án ASP.NET Core Web API chịu trách nhiệm xử lý logic nghiệp vụ, quản lý dữ liệu và cung cấp các RESTful API chuẩn hóa.
* **`MiniSupermarket.WinForms` (Frontend Client):** Ứng dụng Windows Forms đóng vai trò là máy trạm POS tại quầy, sử dụng `HttpClient` để gọi dữ liệu từ API qua mạng và hiển thị trực quan lên `DataGridView`.

---

## 🛠️ 2. Công nghệ Sử dụng
* **Ngôn ngữ:** C# (.NET 8.0)
* **Backend:** ASP.NET Core Web API, Controllers, In-Memory Data, LINQ
* **Frontend:** Windows Forms (.NET 8.0), `System.Net.Http.Json`
* **Công cụ kiểm thử:** Swagger UI

---

## 📂 3. Cấu trúc Solution
```text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/          # Dự án Web API (Backend)
│   ├── Controllers/              # Chứa CategoriesController (CRUD & Search)
│   ├── Models/                   # Chứa lớp thực thể Category.cs
│   └── Program.cs                # Cấu hình dịch vụ và Middleware
│
└── MiniSupermarket.WinForms/     # Dự án Windows Forms (Frontend Client)
    └── FormCategoryManagement.cs # Giao diện quản lý danh mục CRUD


🚀 4. Hướng dẫn Chạy và Kiểm thử Dự án
Bước 1: Chạy phía Backend (Web API)
Mở Solution bằng Visual Studio 2022.


Nhấp chuột phải vào project MiniSupermarket.API chọn Set as Startup Project.


Nhấn F5 để chạy. Trình duyệt sẽ tự động mở giao diện Swagger UI để kiểm tra các phương thức GET, POST, PUT, DELETE.


Bước 2: Chạy phía Frontend (WinForms Client)
Đảm bảo cổng (Port) trong ApiClientService hoặc HttpClient của WinForms khớp với cổng https://localhost:XXXXX của Web API đang chạy.

Nhấp chuột phải vào project MiniSupermarket.WinForms chọn Debug -> Start new instance.

Thử nghiệm các chức năng: Tải danh sách, Thêm mới, Sửa, Xóa và Tìm kiếm nhóm hàng.

👨‍💻 5. Tác giả
Họ tên sinh viên: [Điền tên của bạn vào đây]


Mã sinh viên: [Điền MSSV]


Lớp học phần: [Điền tên lớp]
