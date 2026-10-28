using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms
{
    // --- CÁC LỚP DTO Hứng dữ liệu từ API (Buổi 6 - Bước 3.3 + Liên kết Category & Brand) ---
    public class ProductItemViewDto
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
    }

    public class CategoryComboDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class BrandComboDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
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
        private int _currentPage = 1;
        private int _pageSize = 5;
        private int _totalPages = 1;
        private bool _isLoadingCategories = false;

        public FormProductManagement()
        {
            InitializeComponent();
        }

        private async void FormProductManagement_Load(object? sender, EventArgs e)
        {
            dgvProducts.AutoGenerateColumns = false;
            await LoadCategoriesComboAsync();
            await LoadBrandsComboAsync();
            await LoadProductsPagedAsync();
        }

        // 1. Nạp danh sách Nhóm hàng (Categories) vào ComboBox
        private async Task LoadCategoriesComboAsync()
        {
            try
            {
                _isLoadingCategories = true;

                var categories = await ApiClientService.Client.GetFromJsonAsync<List<CategoryComboDto>>("categories")
                                 ?? new List<CategoryComboDto>();

                cboCategory.DataSource = categories.ToList();
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";

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

        // 1.2. Nạp danh sách Thương hiệu (Brands) vào ComboBox
        private async Task LoadBrandsComboAsync()
        {
            try
            {
                var brands = await ApiClientService.Client.GetFromJsonAsync<List<BrandComboDto>>("brands")
                             ?? new List<BrandComboDto>();

                cboBrand.DataSource = brands.ToList();
                cboBrand.DisplayMember = "BrandName";
                cboBrand.ValueMember = "BrandId";
            }
            catch
            {
                // Fallback nếu chưa có dữ liệu Brand
            }
        }

        // 2. Gọi API Phân trang & Tìm kiếm & Lọc nhóm hàng
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

                string url = $"products/paged?keyword={Uri.EscapeDataString(keyword)}&pageNumber={_currentPage}&pageSize={_pageSize}";
                if (categoryId > 0)
                {
                    url += $"&categoryId={categoryId}";
                }

                var result = await ApiClientService.Client.GetFromJsonAsync<PagedViewResult<ProductItemViewDto>>(url);

                if (result != null)
                {
                    dgvProducts.DataSource = null;
                    dgvProducts.DataSource = result.Items;

                    _totalPages = result.TotalPages == 0 ? 1 : result.TotalPages;
                    lblPaging.Text = $"Trang {result.CurrentPage} / {_totalPages} (Tổng: {result.TotalItems} SP)";

                    btnPrev.Enabled = _currentPage > 1;
                    btnNext.Enabled = _currentPage < _totalPages;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        private async void cboFilterCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isLoadingCategories) return;
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

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

                if (row.Cells["CategoryId"].Value != null)
                {
                    cboCategory.SelectedValue = Convert.ToInt32(row.Cells["CategoryId"].Value);
                }

                if (row.Cells["BrandId"]?.Value != null && cboBrand.Items.Count > 0)
                {
                    cboBrand.SelectedValue = Convert.ToInt32(row.Cells["BrandId"].Value);
                }
            }
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã vạch và Tên sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("Đơn giá bán sản phẩm phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                CategoryId = Convert.ToInt32(cboCategory.SelectedValue),
                BrandId = cboBrand.SelectedValue != null ? Convert.ToInt32(cboBrand.SelectedValue) : 1
            };

            var response = await ApiClientService.Client.PostAsJsonAsync("products", newProduct);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsPagedAsync();
                ClearInputs();
            }
            else
            {
                string err = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Lỗi thêm mới: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("Đơn giá bán sản phẩm phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                BrandId = cboBrand.SelectedValue != null ? Convert.ToInt32(cboBrand.SelectedValue) : 1
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

        private async void btnLoad_Click(object? sender, EventArgs e)
        {
            txtSearchBarcode.Clear();
            _currentPage = 1;
            await LoadCategoriesComboAsync();
            await LoadBrandsComboAsync();
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
            if (cboBrand.Items.Count > 0)
                cboBrand.SelectedIndex = 0;
        }
    }
}
