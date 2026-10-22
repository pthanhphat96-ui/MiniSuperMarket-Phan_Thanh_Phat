namespace MiniSupermarket.WinForms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelBanner = new Panel();
            lblBrandIcon = new Label();
            lblBrandTitle = new Label();
            lblBrandSub = new Label();
            grpLogin = new GroupBox();
            lblFormHeader = new Label();
            btnLogin = new Button();
            txtPass = new TextBox();
            lblPass = new Label();
            txtUser = new TextBox();
            lblUser = new Label();
            lblHint = new Label();
            panelBanner.SuspendLayout();
            grpLogin.SuspendLayout();
            SuspendLayout();

            // panelBanner (Left Navy Panel)
            panelBanner.BackColor = Color.FromArgb(24, 30, 48);
            panelBanner.Dock = DockStyle.Left;
            panelBanner.Location = new Point(0, 0);
            panelBanner.Name = "panelBanner";
            panelBanner.Size = new Size(260, 380);
            panelBanner.Controls.Add(lblBrandSub);
            panelBanner.Controls.Add(lblBrandTitle);
            panelBanner.Controls.Add(lblBrandIcon);

            lblBrandIcon.AutoSize = false;
            lblBrandIcon.Font = new Font("Segoe UI Emoji", 42F, FontStyle.Regular);
            lblBrandIcon.ForeColor = Color.White;
            lblBrandIcon.Location = new Point(0, 75);
            lblBrandIcon.Size = new Size(260, 85);
            lblBrandIcon.Text = "🛒";
            lblBrandIcon.TextAlign = ContentAlignment.MiddleCenter;

            lblBrandTitle.AutoSize = false;
            lblBrandTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBrandTitle.ForeColor = Color.White;
            lblBrandTitle.Location = new Point(0, 165);
            lblBrandTitle.Size = new Size(260, 40);
            lblBrandTitle.Text = "MINIMART POS";
            lblBrandTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblBrandSub.AutoSize = false;
            lblBrandSub.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblBrandSub.ForeColor = Color.FromArgb(148, 163, 184);
            lblBrandSub.Location = new Point(15, 210);
            lblBrandSub.Size = new Size(230, 60);
            lblBrandSub.Text = "Hệ thống Quản lý Bán lẻ &\nThu ngân Siêu thị Mini";
            lblBrandSub.TextAlign = ContentAlignment.TopCenter;

            // grpLogin (Right Login Area)
            grpLogin.BackColor = Color.White;
            grpLogin.FlatStyle = FlatStyle.Flat;
            grpLogin.Controls.Add(lblFormHeader);
            grpLogin.Controls.Add(btnLogin);
            grpLogin.Controls.Add(txtPass);
            grpLogin.Controls.Add(lblPass);
            grpLogin.Controls.Add(txtUser);
            grpLogin.Controls.Add(lblUser);
            grpLogin.Controls.Add(lblHint);
            grpLogin.Location = new Point(285, 25);
            grpLogin.Name = "grpLogin";
            grpLogin.Size = new Size(340, 330);
            grpLogin.TabIndex = 0;
            grpLogin.TabStop = false;
            grpLogin.Text = "";

            lblFormHeader.AutoSize = true;
            lblFormHeader.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblFormHeader.ForeColor = Color.FromArgb(30, 41, 59);
            lblFormHeader.Location = new Point(25, 25);
            lblFormHeader.Text = "ĐĂNG NHẬP HỆ THỐNG";

            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUser.ForeColor = Color.FromArgb(71, 85, 105);
            lblUser.Location = new Point(25, 75);
            lblUser.Name = "lblUser";
            lblUser.Text = "Tài khoản đăng nhập";

            txtUser.Font = new Font("Segoe UI", 10.5F);
            txtUser.Location = new Point(25, 100);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Nhập tài khoản (vd: admin)...";
            txtUser.Size = new Size(290, 29);
            txtUser.TabIndex = 1;

            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPass.ForeColor = Color.FromArgb(71, 85, 105);
            lblPass.Location = new Point(25, 145);
            lblPass.Name = "lblPass";
            lblPass.Text = "Mật khẩu";

            txtPass.Font = new Font("Segoe UI", 10.5F);
            txtPass.Location = new Point(25, 170);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "Nhập mật khẩu...";
            txtPass.Size = new Size(290, 29);
            txtPass.TabIndex = 2;
            txtPass.UseSystemPasswordChar = true;

            btnLogin.BackColor = Color.FromArgb(37, 99, 235);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(25, 225);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(290, 42);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "ĐĂNG NHẬP HỆ THỐNG";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += btnLogin_Click;

            lblHint.AutoSize = true;
            lblHint.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblHint.ForeColor = Color.FromArgb(100, 116, 139);
            lblHint.Location = new Point(25, 285);
            lblHint.Text = "Gợi ý: admin / 123456 hoặc cashier01 / 123456";

            // FormLogin
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(650, 380);
            Controls.Add(grpLogin);
            Controls.Add(panelBanner);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MiniMart POS - Đăng nhập hệ thống";
            panelBanner.ResumeLayout(false);
            grpLogin.ResumeLayout(false);
            grpLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBanner;
        private Label lblBrandIcon;
        private Label lblBrandTitle;
        private Label lblBrandSub;
        private GroupBox grpLogin;
        private Label lblFormHeader;
        private Label lblUser;
        private TextBox txtUser;
        private Label lblPass;
        private TextBox txtPass;
        private Button btnLogin;
        private Label lblHint;
    }
}
