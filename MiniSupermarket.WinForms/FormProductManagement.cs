using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms
{
    // --- CÁC LỚP DTO Hứng dữ liệu từ API (Buổi 6 - Bước 3.3) ---
    public class ProductItemViewDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class CategoryComboDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class PagedViewResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }

    public partial class FormProductManagement : Form
    {
        // Biến lưu trữ trạng thái phân trang hiện tại (Buổi 6 - Bước 3.3)
        private int _currentPage = 1;
        private int _pageSize = 5; // Hiển thị 5 sản phẩm 1 trang để dễ test phân trang
        private int _totalPages = 1;
        private bool _isLoadingCategories = false;

        public FormProductManagement()
        {
            InitializeComponent();
        }

        // Sự kiện khi Form vừa mở lên
        private async void FormProductManagement_Load(object? sender, EventArgs e)
        {
            dgvProducts.AutoGenerateColumns = false;
            await LoadCategoriesComboAsync(); // Phải nạp ComboBox trước
            await LoadProductsPagedAsync();   // Sau đó mới nạp bảng Sản phẩm phân trang
        }

        // 1. Hàm gọi API lấy danh sách Nhóm hàng đổ vào ComboBox (cboCategory & cboFilterCategory)
        private async Task LoadCategoriesComboAsync()
        {
            try
            {
                _isLoadingCategories = true;

                var categories = await ApiClientService.Client.GetFromJsonAsync<List<CategoryComboDto>>("categories")
                                 ?? new List<CategoryComboDto>();

                // Nạp vào ComboBox chi tiết sản phẩm bên phải (cboCategory)
                cboCategory.DataSource = categories.ToList();
                cboCategory.DisplayMember = "CategoryName"; // Hiển thị Tên cho người dùng xem
                cboCategory.ValueMember = "CategoryId";     // Lưu ngầm ID bên dưới để gửi về DB

                // Bài tập 6.2: Nạp vào ComboBox lọc nhóm hàng phía trên (cboFilterCategory)
                var filterList = new List<CategoryComboDto>
                {
                    new CategoryComboDto { CategoryId = 0, CategoryName = "-- Tất cả nhóm hàng --" }
                };
                filterList.AddRange(categories);

                cboFilterCategory.DataSource = filterList;
                cboFilterCategory.DisplayMember = "CategoryName";
                cboFilterCategory.ValueMember = "CategoryId";
                cboFilterCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingCategories = false;
            }
        }

        // 2. Hàm gọi API Phân trang & Tìm kiếm & Lọc nhóm hàng (Buổi 6 - Bước 3.3 + Bài tập 6.2)
        private async Task LoadProductsPagedAsync()
        {
            try
            {
                string keyword = txtSearchBarcode.Text.Trim();
                int categoryId = 0;

                if (cboFilterCategory.SelectedValue is int val)
                {
                    categoryId = val;
                }
                else if (cboFilterCategory.SelectedItem is CategoryComboDto dto)
                {
                    categoryId = dto.CategoryId;
                }

                // Ghép chuỗi Query String gửi lên Server
                string url = $"products/paged?keyword={Uri.EscapeDataString(keyword)}&pageNumber={_currentPage}&pageSize={_pageSize}";
                if (categoryId > 0)
                {
                    url += $"&categoryId={categoryId}";
                }

                var result = await ApiClientService.Client.GetFromJsonAsync<PagedViewResult<ProductItemViewDto>>(url);

                if (result != null)
                {
                    // Đổ dữ liệu 5 dòng vào Bảng
                    dgvProducts.DataSource = null;
                    dgvProducts.DataSource = result.Items;

                    // Cập nhật trạng thái Phân trang
                    _totalPages = result.TotalPages == 0 ? 1 : result.TotalPages;
                    lblPaging.Text = $"Trang {result.CurrentPage} / {_totalPages} (Tổng: {result.TotalItems} SP)";

                    // Bật/Tắt nút chuyển trang hợp lý
                    btnPrev.Enabled = _currentPage > 1;
                    btnNext.Enabled = _currentPage < _totalPages;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CÁC SỰ KIỆN NÚT BẤM PHÂN TRANG & TÌM KIẾM ---
        private async void btnPrev_Click(object? sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadProductsPagedAsync();
            }
        }

        private async void btnNext_Click(object? sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await LoadProductsPagedAsync();
            }
        }

        private async void btnSearch_Click(object? sender, EventArgs e)
        {
            _currentPage = 1; // Khi bấm tìm kiếm mới, phải đưa trang về 1
            await LoadProductsPagedAsync();
        }

        // Bài tập 6.2: Khi chọn mục khác trong ComboBox lọc nhóm hàng -> tự động lọc lại ở trang 1
        private async void cboFilterCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isLoadingCategories) return;
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        // --- CÁC SỰ KIỆN CRUD (THÊM, SỬA, XÓA) ---
        // Bấm vào 1 dòng trên lưới -> Đổ dữ liệu lên các ô nhập liệu và chọn đúng ComboBox
        private void dgvProducts_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvProducts.Rows[e.RowIndex];
                txtId.Text = row.Cells["ProductId"].Value?.ToString() ?? string.Empty;
                txtBarcode.Text = row.Cells["Barcode"].Value?.ToString() ?? string.Empty;
                txtProductName.Text = row.Cells["ProductName"].Value?.ToString() ?? string.Empty;
                nudPrice.Value = Convert.ToDecimal(row.Cells["Price"].Value ?? 0);
                nudStock.Value = Convert.ToInt32(row.Cells["StockQuantity"].Value ?? 0);

                // Tự động nhảy ComboBox về đúng Nhóm hàng của sản phẩm đang chọn
                if (row.Cells["CategoryId"].Value != null)
                {
                    cboCategory.SelectedValue = Convert.ToInt32(row.Cells["CategoryId"].Value);
                }
            }
        }

        // Nút Thêm mới Sản phẩm
        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã vạch và Tên sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Nhóm hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newProduct = new
            {
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = (int)cboCategory.SelectedValue, // Lấy ID từ ComboBox
                BrandId = 1
            };

            var response = await ApiClientService.Client.PostAsJsonAsync("products", newProduct);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsPagedAsync(); // Tải lại bảng
                ClearInputs();
            }
            else
            {
                string err = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Lỗi thêm mới: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Cập nhật Sản phẩm
        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);

            var updateProd = new
            {
                ProductId = id,
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = cboCategory.SelectedValue != null ? Convert.ToInt32(cboCategory.SelectedValue) : 1,
                BrandId = 1
            };

            var res = await ApiClientService.Client.PutAsJsonAsync($"products/{id}", updateProd);
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsPagedAsync();
            }
            else
            {
                string err = await res.Content.ReadAsStringAsync();
                MessageBox.Show("Cập nhật thất bại!\n" + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Xóa Sản phẩm
        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);

            if (MessageBox.Show($"Xác nhận xóa sản phẩm ID = {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var res = await ApiClientService.Client.DeleteAsync($"products/{id}");
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Đã xóa sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadProductsPagedAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại (Sản phẩm đã có trong hóa đơn)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Nút Làm mới
        private async void btnLoad_Click(object? sender, EventArgs e)
        {
            txtSearchBarcode.Clear();
            _currentPage = 1;
            await LoadCategoriesComboAsync();
            await LoadProductsPagedAsync();
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            nudPrice.Value = 0;
            nudStock.Value = 0;
            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;
        }
    }
}
