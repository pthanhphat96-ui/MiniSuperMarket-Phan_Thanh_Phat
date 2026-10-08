using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        // Form con đang được mở
        private Form? _activeForm = null;

        public FormMainShell()
        {
            InitializeComponent();
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private void FormMainShell_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin tài khoản đang đăng nhập
            lblUserInfo.Text =
                $"Nhân viên: {SessionManager.CurrentUsername} | " +
                $"Vai trò: [{SessionManager.CurrentRole}]";

            // Phân quyền menu
            ApplyRolePermissions(SessionManager.CurrentRole);

            // Mở màn hình mặc định theo vai trò
            OpenDefaultScreenByRole(SessionManager.CurrentRole);
        }

        // =========================================================
        // MỞ FORM CON TRONG PANEL MAIN
        // =========================================================

        /// <summary>
        /// Nhúng Form con vào panelMainContent
        /// </summary>
        private void OpenChildForm(
            Form childForm,
            string screenTitle,
            Button senderButton)
        {
            // Nếu đang có Form con thì đóng Form cũ
            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
                _activeForm = null;
            }

            // Highlight nút đang được chọn
            HighlightActiveButton(senderButton);

            // Lưu Form hiện tại
            _activeForm = childForm;

            // Chuyển Form thành Control con
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Xóa nội dung cũ
            panelMainContent.Controls.Clear();

            // Thêm Form mới vào panel
            panelMainContent.Controls.Add(childForm);
            panelMainContent.Tag = childForm;

            // Đổi tiêu đề Header
            lblTitle.Text = screenTitle;

            // Hiển thị Form
            childForm.BringToFront();
            childForm.Show();
        }

        // =========================================================
        // HIGHLIGHT BUTTON ĐANG CHỌN
        // =========================================================

        private void HighlightActiveButton(Button activeButton)
        {
            foreach (Control ctrl in panelSidebar.Controls)
            {
                if (ctrl is Button btn && btn != btnLogout)
                {
                    btn.BackColor =
                        Color.FromArgb(24, 30, 48);
                }
            }

            activeButton.BackColor =
                Color.FromArgb(41, 100, 180);
        }

        // =========================================================
        // PHÂN QUYỀN THEO ROLE
        // =========================================================

        /// <summary>
        /// Phân định quyền truy cập hiển thị theo vai trò người dùng
        /// </summary>
        private void ApplyRolePermissions(string role)
        {
            switch (role.ToUpper())
            {
                // -------------------------------------------------
                // ADMIN
                // -------------------------------------------------

                case "ADMIN":

                    // Quản trị viên:
                    // Có toàn quyền sử dụng tất cả các chức năng

                    btnPOS.Visible = true;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnReports.Visible = true;
                    btnUserManage.Visible = true;

                    break;


                // -------------------------------------------------
                // CASHIER
                // -------------------------------------------------

                case "CASHIER":

                    // Thu ngân:
                    // Chỉ được Bán hàng và Quản lý khách hàng

                    btnPOS.Visible = true;
                    btnCustomer.Visible = true;

                    btnCategory.Visible = false;
                    btnProduct.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;

                    break;


                // -------------------------------------------------
                // WAREHOUSE
                // -------------------------------------------------

                case "WAREHOUSE":

                    // Thủ kho:
                    // Chỉ quản lý Danh mục và Sản phẩm

                    btnPOS.Visible = false;
                    btnCustomer.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;

                    btnCategory.Visible = true;
                    btnProduct.Visible = true;

                    break;


                // -------------------------------------------------
                // ROLE KHÔNG HỢP LỆ
                // -------------------------------------------------

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

        // =========================================================
        // MỞ MÀN HÌNH MẶC ĐỊNH THEO ROLE
        // =========================================================

        /// <summary>
        /// Điều hướng ngay vào màn hình đúng chuyên môn
        /// của từng vai trò
        /// </summary>
        private void OpenDefaultScreenByRole(string role)
        {
            switch (role.ToUpper())
            {
                // ADMIN:
                // Mở quản lý danh mục
                case "ADMIN":

                    OpenChildForm(
                        new FormCategoryManagement(),
                        "QUẢN LÝ DANH MỤC NHÓM HÀNG",
                        btnCategory
                    );

                    break;


                // WAREHOUSE:
                // Mở quản lý danh mục
                case "WAREHOUSE":

                    OpenChildForm(
                        new FormCategoryManagement(),
                        "QUẢN LÝ DANH MỤC NHÓM HÀNG",
                        btnCategory
                    );

                    break;


                // CASHIER:
                // Mở quản lý khách hàng
                case "CASHIER":

                    OpenChildForm(
                        new FormCustomerManagement(),
                        "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM",
                        btnCustomer
                    );

                    break;
            }
        }

        // =========================================================
        // BUTTON: DANH MỤC
        // =========================================================

        private void btnCategory_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormCategoryManagement(),
                "QUẢN LÝ DANH MỤC SẢN PHẨM",
                btnCategory
            );
        }

        // =========================================================
        // BUTTON: SẢN PHẨM
        // =========================================================

        private void btnProduct_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormProductManagement(),
                "QUẢN LÝ SẢN PHẨM",
                btnProduct
            );
        }

        // =========================================================
        // BUTTON: KHÁCH HÀNG
        // =========================================================

        private void btnCustomer_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormCustomerManagement(),
                "QUẢN LÝ KHÁCH HÀNG THÂN THIẾT",
                btnCustomer
            );
        }

        // =========================================================
        // BUTTON: POS
        // =========================================================

        private void btnPOS_Click(
            object sender,
            EventArgs e)
        {
            // Hiện tại POS chưa hoàn thiện
            MessageBox.Show(
                "Màn hình Quét mã vạch Barcode POS sẵn sàng!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            /*
            // Sau này khi có FormPOS thì thay bằng:

            OpenChildForm(
                new FormPOS(),
                "BÁN HÀNG (POS)",
                btnPOS
            );
            */
        }

        // =========================================================
        // BUTTON: BÁO CÁO
        // =========================================================

        private void btnReports_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormReportManagement(),
                "BÁO CÁO DOANH THU",
                btnReports
            );
        }

        // =========================================================
        // BUTTON: QUẢN TRỊ TÀI KHOẢN
        // =========================================================

        private void btnUserManage_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormUserManagement(),
                "QUẢN TRỊ TÀI KHOẢN",
                btnUserManage
            );
        }

        // =========================================================
        // BUTTON: ĐĂNG XUẤT
        // =========================================================

        private void btnLogout_Click(
            object sender,
            EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất phiên làm việc?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            // Xóa thông tin phiên đăng nhập
            SessionManager.JwtToken = string.Empty;
            SessionManager.CurrentUsername = string.Empty;
            SessionManager.CurrentRole = string.Empty;

            // Đóng Form con
            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
                _activeForm = null;
            }

            // Ẩn Main Shell
            this.Hide();

            // Mở lại Login
            using (FormLogin login = new FormLogin())
            {
                login.ShowDialog();
            }

            // Đóng Main Shell
            this.Close();
        }
    }
}