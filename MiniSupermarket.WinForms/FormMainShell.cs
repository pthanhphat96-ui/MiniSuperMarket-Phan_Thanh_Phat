using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        private Form? _activeForm = null;

        public FormMainShell()
        {
            InitializeComponent();
            panelMainContent.AutoScroll = false;
        }

        private void FormMainShell_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text =
                $"👤 Nhân viên: {SessionManager.CurrentUsername}   |   🔑 Vai trò: [{SessionManager.CurrentRole}]";

            ApplyRolePermissions(SessionManager.CurrentRole);
            OpenDefaultScreenByRole(SessionManager.CurrentRole);
        }

        private void OpenChildForm(
            Form childForm,
            string screenTitle,
            Button senderButton)
        {
            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
                _activeForm = null;
            }

            HighlightActiveButton(senderButton);

            _activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.AutoScroll = false;
            childForm.Dock = DockStyle.Fill;

            panelMainContent.SuspendLayout();
            panelMainContent.Controls.Clear();
            panelMainContent.Controls.Add(childForm);
            panelMainContent.Tag = childForm;
            panelMainContent.ResumeLayout(true);

            lblTitle.Text = screenTitle;

            childForm.BringToFront();
            childForm.Show();
        }

        private void HighlightActiveButton(Button activeButton)
        {
            foreach (Control ctrl in panelSidebar.Controls)
            {
                if (ctrl is Button btn && btn != btnLogout)
                {
                    btn.BackColor = Color.FromArgb(24, 30, 48);
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }

            activeButton.BackColor = Color.FromArgb(37, 99, 235);
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void ApplyRolePermissions(string role)
        {
            switch (role.ToUpper())
            {
                case "ADMIN":
                    btnPOS.Visible = true;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnReports.Visible = true;
                    btnUserManage.Visible = true;
                    break;

                case "CASHIER":
                    btnPOS.Visible = true;
                    btnCustomer.Visible = true;
                    btnCategory.Visible = false;
                    btnProduct.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;
                    break;

                case "WAREHOUSE":
                    btnPOS.Visible = false;
                    btnCustomer.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    break;

                default:
                    MessageBox.Show(
                        "Tài khoản chưa được cấp quyền hạn hợp lệ!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    this.Close();
                    break;
            }
        }

        private void OpenDefaultScreenByRole(string role)
        {
            switch (role.ToUpper())
            {
                case "ADMIN":
                    OpenChildForm(
                        new FormCategoryManagement(),
                        "QUẢN LÝ DANH MỤC NHÓM HÀNG",
                        btnCategory
                    );
                    break;

                case "WAREHOUSE":
                    OpenChildForm(
                        new FormCategoryManagement(),
                        "QUẢN LÝ DANH MỤC NHÓM HÀNG",
                        btnCategory
                    );
                    break;

                case "CASHIER":
                    OpenChildForm(
                        new FormCustomerManagement(),
                        "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM",
                        btnCustomer
                    );
                    break;
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(
                new FormCategoryManagement(),
                "QUẢN LÝ DANH MỤC NHÓM HÀNG",
                btnCategory
            );
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(
                new FormProductManagement(),
                "QUẢN LÝ SẢN PHẨM & KHO HÀNG",
                btnProduct
            );
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            OpenChildForm(
                new FormCustomerManagement(),
                "QUẢN LÝ KHÁCH HÀNG THÂN THIẾT",
                btnCustomer
            );
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            OpenChildForm(
                new FormPOS(),
                "QUẦY THU NGÂN BÁN HÀNG (POS)",
                btnPOS
            );
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenChildForm(
                new FormReport(),
                "BÁO CÁO & THỐNG KÊ DOANH THU",
                btnReports
            );
        }

        private void btnUserManage_Click(object sender, EventArgs e)
        {
            OpenChildForm(
                new FormUserManagement(),
                "QUẢN TRỊ TÀI KHOẢN & PHÂN QUYỀN",
                btnUserManage
            );
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất phiên làm việc?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            SessionManager.JwtToken = string.Empty;
            SessionManager.CurrentUsername = string.Empty;
            SessionManager.CurrentRole = string.Empty;

            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
                _activeForm = null;
            }

            this.Hide();

            using (FormLogin login = new FormLogin())
            {
                login.ShowDialog();
            }

            this.Close();
        }
    }
}
