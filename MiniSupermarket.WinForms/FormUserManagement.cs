using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MiniSupermarket.WinForms.SessionManager;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        private int _selectedUserId = 0;
        private bool _selectedIsActive = true;

        public FormUserManagement()
        {
            InitializeComponent();

            cboRole.Items.AddRange(new string[]
            {
                "Admin",
                "Cashier",
                "Warehouse"
            });

            cboRole.SelectedIndex = 1;
        }

        private async void FormUserManagement_Load(object? sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await ApiClientService.Client.GetFromJsonAsync<List<UserDto>>("users")
                            ?? new List<UserDto>();

                dgvUsers.AutoGenerateColumns = false;
                dgvUsers.DataSource = null;
                dgvUsers.DataSource = users;

                // Tô màu trực quan cho tài khoản bị khóa / đang hoạt động
                foreach (DataGridViewRow row in dgvUsers.Rows)
                {
                    if (row.DataBoundItem is UserDto u)
                    {
                        if (!u.IsActive)
                        {
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                            row.DefaultCellStyle.BackColor = Color.FromArgb(254, 242, 242);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lấy danh sách tài khoản: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnAddUser_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Tên đăng nhập và mật khẩu không được trống!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var newUser = new
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Role = cboRole.SelectedItem?.ToString() ?? "Cashier",
                IsActive = true
            };

            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync(
                    "users",
                    newUser);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Tạo tài khoản mới thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadUsersAsync();
                    ClearInput();
                }
                else
                {
                    var message = await res.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Tạo tài khoản thất bại!\n" + message,
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnUpdateUser_Click(object? sender, EventArgs e)
        {
            if (_selectedUserId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn một tài khoản trên bảng để cập nhật!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var updateDto = new
            {
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Password = string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text.Trim(),
                Role = cboRole.SelectedItem?.ToString() ?? "Cashier",
                IsActive = _selectedIsActive
            };

            try
            {
                var res = await ApiClientService.Client.PutAsJsonAsync(
                    $"users/{_selectedUserId}",
                    updateDto);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật thông tin tài khoản thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadUsersAsync();
                }
                else
                {
                    var msg = await res.Content.ReadAsStringAsync();
                    MessageBox.Show("Cập nhật thất bại:\n" + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnResetPassword_Click(object? sender, EventArgs e)
        {
            int userId = GetSelectedUserId();
            if (userId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản trên bảng cần đặt lại mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string newPassword = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu mới vào ô Mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            var data = new
            {
                Password = newPassword
            };

            try
            {
                var res = await ApiClientService.Client.PutAsJsonAsync(
                    $"users/{userId}/reset-password",
                    data);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Đặt lại mật khẩu thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtPassword.Clear();
                }
                else
                {
                    string err = await res.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        "Không thể đặt lại mật khẩu!\n" + err,
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnToggleLock_Click(object? sender, EventArgs e)
        {
            int userId = GetSelectedUserId();
            if (userId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn một tài khoản trên danh sách để Khóa / Mở khóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            bool isActive = _selectedIsActive;
            if (dgvUsers.CurrentRow?.DataBoundItem is UserDto boundUser)
            {
                isActive = boundUser.IsActive;
            }

            string username = txtUsername.Text;
            string actionName = isActive ? "KHÓA" : "MỞ KHÓA";

            if (MessageBox.Show(
                    $"Bạn có chắc chắn muốn {actionName} tài khoản '{username}' (ID: {userId})?",
                    "Xác nhận trạng thái tài khoản",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var data = new
                {
                    IsActive = !isActive
                };

                var res = await ApiClientService.Client.PutAsJsonAsync(
                    $"users/{userId}/toggle-status",
                    data);

                if (res.IsSuccessStatusCode)
                {
                    _selectedIsActive = !isActive;
                    UpdateLockButtonAppearance(_selectedIsActive);

                    MessageBox.Show(
                        !isActive
                            ? $"Đã MỞ KHÓA tài khoản '{username}' thành công!"
                            : $"Đã KHÓA tài khoản '{username}' thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadUsersAsync();
                }
                else
                {
                    string err = await res.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        "Không thể thay đổi trạng thái tài khoản!\n" + err,
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private int GetSelectedUserId()
        {
            if (_selectedUserId > 0) return _selectedUserId;

            if (dgvUsers.CurrentRow?.DataBoundItem is UserDto u)
            {
                return u.EffectiveId;
            }

            if (dgvUsers.CurrentRow?.Cells["Id"]?.Value != null)
            {
                return Convert.ToInt32(dgvUsers.CurrentRow.Cells["Id"].Value);
            }

            return 0;
        }

        private void UpdateLockButtonAppearance(bool isActive)
        {
            if (isActive)
            {
                btnToggleLock.Text = "🔒 Khóa tài khoản đang chọn";
                btnToggleLock.BackColor = Color.FromArgb(220, 38, 38);
            }
            else
            {
                btnToggleLock.Text = "🔓 Mở khóa tài khoản đang chọn";
                btnToggleLock.BackColor = Color.FromArgb(22, 163, 74);
            }
        }

        private void dgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgvUsers.Rows[e.RowIndex];

            if (row.DataBoundItem is UserDto u)
            {
                _selectedUserId = u.EffectiveId;
                _selectedIsActive = u.IsActive;
                txtUsername.Text = u.Username;
                txtFullName.Text = u.FullName;
                txtEmail.Text = u.Email;
                txtPhone.Text = u.Phone;
                txtPassword.Clear();

                if (cboRole.Items.Contains(u.Role))
                    cboRole.SelectedItem = u.Role;

                UpdateLockButtonAppearance(u.IsActive);
            }
        }

        private async void btnReload_Click(object? sender, EventArgs e)
        {
            ClearInput();
            await LoadUsersAsync();
        }

        private void ClearInput()
        {
            _selectedUserId = 0;
            _selectedIsActive = true;
            txtUsername.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = 1;
            btnToggleLock.Text = "🔒 Khóa / Mở khóa tài khoản";
            btnToggleLock.BackColor = Color.FromArgb(217, 119, 6);
        }
    }

    public class UserDto
    {
        [Browsable(false)]
        public int Id { get; set; }

        [Browsable(false)]
        public int UserId { get; set; }

        public int EffectiveId => Id > 0 ? Id : UserId;

        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        [Browsable(false)]
        public int RoleId { get; set; }

        public string Role { get; set; } = string.Empty;

        [Browsable(false)]
        public bool IsActive { get; set; }

        public string StatusText => IsActive ? "🟢 Đang hoạt động" : "🔴 Đã khóa";
    }
}
