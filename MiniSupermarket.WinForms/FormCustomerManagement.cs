using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        private readonly HttpClient _httpClient;

        private const string ApiUrl =
            "https://localhost:7158/api/customers";

        // Biến lưu trạng thái Phân trang Khách hàng (Bài tập 6.1 - Buổi 6)
        private int _currentPage = 1;
        private int _pageSize = 5;
        private int _totalPages = 1;

        public FormCustomerManagement()
        {
            InitializeComponent();

            _httpClient = new HttpClient();

            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }

            // Đăng ký sự kiện
            this.Load += FormCustomerManagement_Load;

            btnLoad.Click += btnLoad_Click;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnSearch.Click += btnSearch_Click;
            btnPrev.Click += btnPrev_Click;
            btnNext.Click += btnNext_Click;

            dgvCustomers.CellClick += dgvCustomers_CellClick;
        }

        public class Customer
        {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string? Address { get; set; }
            public int RewardPoints { get; set; }
            public string MembershipRank { get; set; } = "Chuẩn";
        }

        private async void FormCustomerManagement_Load(
            object? sender,
            EventArgs e)
        {
            txtCustomerId.ReadOnly = true;
            txtRewardPoints.Text = "0";
            txtMembershipRank.Text = "Chuẩn";

            await LoadCustomersPagedAsync();
        }

        // =========================================================
        // 1. GET PAGED CUSTOMERS (Bài tập 6.1 - Buổi 6)
        // GET /api/customers/paged?keyword=...&pageNumber=1&pageSize=5
        // =========================================================
        private async Task LoadCustomersPagedAsync(string? keyword = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionManager.JwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
                }

                string kw = keyword ?? string.Empty;
                string url = $"{ApiUrl}/paged?keyword={Uri.EscapeDataString(kw)}&pageNumber={_currentPage}&pageSize={_pageSize}";

                var result = await _httpClient.GetFromJsonAsync<PagedViewResult<Customer>>(url);

                if (result != null)
                {
                    dgvCustomers.DataSource = null;
                    dgvCustomers.DataSource = result.Items;
                    ConfigureDataGridView();

                    _totalPages = result.TotalPages == 0 ? 1 : result.TotalPages;
                    lblPaging.Text = $"Trang {result.CurrentPage} / {_totalPages} (Tổng: {result.TotalItems} KH)";

                    btnPrev.Enabled = _currentPage > 1;
                    btnNext.Enabled = _currentPage < _totalPages;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Không thể kết nối đến API!\n\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnPrev_Click(object? sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadCustomersPagedAsync(txtCustomerName.Text.Trim());
            }
        }

        private async void btnNext_Click(object? sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await LoadCustomersPagedAsync(txtCustomerName.Text.Trim());
            }
        }

        private void ConfigureDataGridView()
        {
            if (dgvCustomers.Columns.Count == 0)
                return;

            if (dgvCustomers.Columns["CustomerId"] != null)
                dgvCustomers.Columns["CustomerId"].HeaderText = "Mã KH";

            if (dgvCustomers.Columns["CustomerName"] != null)
                dgvCustomers.Columns["CustomerName"].HeaderText = "Tên khách hàng";

            if (dgvCustomers.Columns["PhoneNumber"] != null)
                dgvCustomers.Columns["PhoneNumber"].HeaderText = "Số điện thoại";

            if (dgvCustomers.Columns["Address"] != null)
                dgvCustomers.Columns["Address"].HeaderText = "Địa chỉ";

            if (dgvCustomers.Columns["RewardPoints"] != null)
                dgvCustomers.Columns["RewardPoints"].HeaderText = "Điểm tích lũy";

            if (dgvCustomers.Columns["MembershipRank"] != null)
                dgvCustomers.Columns["MembershipRank"].HeaderText = "Hạng thành viên";

            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
        }

        private async void btnLoad_Click(
            object? sender,
            EventArgs e)
        {
            ClearInput();
            _currentPage = 1;
            await LoadCustomersPagedAsync();
        }

        private async void btnAdd_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (!ValidateCustomerInput())
                    return;

                var customer = new Customer
                {
                    CustomerName = txtCustomerName.Text.Trim(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                    RewardPoints = 0,
                    MembershipRank = "Chuẩn"
                };

                var response = await _httpClient.PostAsJsonAsync(ApiUrl, customer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ClearInput();
                    await LoadCustomersPagedAsync();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        "Thêm khách hàng thất bại!\n\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi thêm khách hàng!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtCustomerId.Text, out int customerId))
                {
                    MessageBox.Show(
                        "Vui lòng chọn khách hàng cần cập nhật!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateCustomerInput())
                    return;

                if (!int.TryParse(txtRewardPoints.Text, out int rewardPoints))
                {
                    MessageBox.Show(
                        "Điểm tích lũy phải là số nguyên!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtRewardPoints.Focus();
                    return;
                }

                var customer = new Customer
                {
                    CustomerId = customerId,
                    CustomerName = txtCustomerName.Text.Trim(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                    RewardPoints = rewardPoints,
                    MembershipRank = txtMembershipRank.Text.Trim()
                };

                string url = $"{ApiUrl}/{customerId}";
                var response = await _httpClient.PutAsJsonAsync(url, customer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ClearInput();
                    await LoadCustomersPagedAsync();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        "Cập nhật khách hàng thất bại!\n\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi cập nhật khách hàng!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtCustomerId.Text, out int customerId))
                {
                    MessageBox.Show(
                        "Vui lòng chọn khách hàng cần xóa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa khách hàng này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                string url = $"{ApiUrl}/{customerId}";
                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ClearInput();
                    await LoadCustomersPagedAsync();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        "Xóa khách hàng thất bại!\n\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi xóa khách hàng!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnSearch_Click(
            object? sender,
            EventArgs e)
        {
            _currentPage = 1;
            string keyword = txtCustomerName.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                keyword = txtPhoneNumber.Text.Trim();
            }
            await LoadCustomersPagedAsync(keyword);
        }

        private void dgvCustomers_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];

            txtCustomerId.Text = row.Cells["CustomerId"].Value?.ToString() ?? "";
            txtCustomerName.Text = row.Cells["CustomerName"].Value?.ToString() ?? "";
            txtPhoneNumber.Text = row.Cells["PhoneNumber"].Value?.ToString() ?? "";
            txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
            txtRewardPoints.Text = row.Cells["RewardPoints"].Value?.ToString() ?? "0";
            txtMembershipRank.Text = row.Cells["MembershipRank"].Value?.ToString() ?? "Chuẩn";
        }

        private bool ValidateCustomerInput()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên khách hàng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập số điện thoại!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return false;
            }

            if (txtPhoneNumber.Text.Trim().Length > 15)
            {
                MessageBox.Show(
                    "Số điện thoại không được vượt quá 15 ký tự!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return false;
            }

            return true;
        }

        private void ClearInput()
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtRewardPoints.Text = "0";
            txtMembershipRank.Text = "Chuẩn";
            txtCustomerName.Focus();
        }

        public void SetJwtToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            _httpClient.Dispose();
            base.OnFormClosed(e);
        }
    }
}
