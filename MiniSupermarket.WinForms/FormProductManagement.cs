using MiniSupermarket.WinForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MiniSupermarket.WinForms.SessionManager;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        private bool _isLoadingCategories = false;
        private List<Models.CategoryDto> _categories = new();

        public FormProductManagement()
        {
            InitializeComponent();
        }

        private async void FormProductManagement_Load(object? sender, EventArgs e)
        {
            dgvProducts.AutoGenerateColumns = false;
            await LoadCategoriesToComboAsync();
            await LoadProductsAsync();
        }

        private async Task LoadCategoriesToComboAsync()
        {
            try
            {
                _isLoadingCategories = true;

                var categories = await ApiClientService.Client.GetFromJsonAsync<List<Models.CategoryDto>>("categories")
                                 ?? new List<Models.CategoryDto>();

                _categories = categories;

                // 1. Nạp danh sách nhóm hàng vào ComboBox chi tiết bên phải (cboCategory)
                cboCategory.DataSource = categories.ToList();
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";

                // 2. Nạp danh sách nhóm hàng vào ComboBox lọc phía trên (cboFilterCategory)
                var filterList = new List<Models.CategoryDto>
                {
                    new Models.CategoryDto { CategoryId = 0, CategoryName = "-- Tất cả nhóm hàng --" }
                };
                filterList.AddRange(categories);

                cboFilterCategory.DataSource = filterList;
                cboFilterCategory.DisplayMember = "CategoryName";
                cboFilterCategory.ValueMember = "CategoryId";
                cboFilterCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục nhóm hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingCategories = false;
            }
        }

        private async Task LoadProductsAsync(string? keyword = null, int? categoryId = null)
        {
            try
            {
                string url = "products";
                if (!string.IsNullOrWhiteSpace(keyword) || (categoryId.HasValue && categoryId.Value > 0))
                {
                    var queryParams = new List<string>();
                    if (!string.IsNullOrWhiteSpace(keyword))
                        queryParams.Add($"keyword={Uri.EscapeDataString(keyword.Trim())}");
                    if (categoryId.HasValue && categoryId.Value > 0)
                        queryParams.Add($"categoryId={categoryId.Value}");

                    url = "products/search?" + string.Join("&", queryParams);
                }

                var products = await ApiClientService.Client.GetFromJsonAsync<List<ProductDto>>(url)
                               ?? new List<ProductDto>();

                // Đảm bảo mỗi sản phẩm đều có CategoryName hiển thị kể cả khi API không trả object Category lồng nhau
                foreach (var p in products)
                {
                    if (p.Category == null && p.CategoryId > 0)
                    {
                        p.Category = _categories.FirstOrDefault(c => c.CategoryId == p.CategoryId);
                    }
                }

                dgvProducts.DataSource = null;
                dgvProducts.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSearch_Click(object? sender, EventArgs e)
        {
            await ApplyFilterAsync();
        }

        private async void cboFilterCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isLoadingCategories) return;
            await ApplyFilterAsync();
        }

        private async Task ApplyFilterAsync()
        {
            string keyword = txtSearchBarcode.Text.Trim();
            int categoryId = 0;
            if (cboFilterCategory.SelectedValue is int val)
            {
                categoryId = val;
            }
            else if (cboFilterCategory.SelectedItem is Models.CategoryDto dto)
            {
                categoryId = dto.CategoryId;
            }

            await LoadProductsAsync(keyword, categoryId);
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
                    int catId = Convert.ToInt32(row.Cells["CategoryId"].Value);
                    cboCategory.SelectedValue = catId;
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

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Nhóm hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newProd = new
            {
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = Convert.ToInt32(cboCategory.SelectedValue),
                BrandId = 1
            };

            var res = await ApiClientService.Client.PostAsJsonAsync("products", newProd);
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsAsync();
                ClearInputs();
            }
            else
            {
                string err = await res.Content.ReadAsStringAsync();
                MessageBox.Show("Thêm mới sản phẩm thất bại!\n" + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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
                await ApplyFilterAsync();
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
                    await ApplyFilterAsync();
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
            if (cboFilterCategory.Items.Count > 0)
                cboFilterCategory.SelectedIndex = 0;

            await LoadCategoriesToComboAsync();
            await LoadProductsAsync();
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
