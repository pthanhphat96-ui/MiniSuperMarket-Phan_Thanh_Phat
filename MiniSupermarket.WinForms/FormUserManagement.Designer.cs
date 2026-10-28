using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox grpUserList;
        private GroupBox panelInput;

        private Label lblUsername;
        private Label lblFullName;
        private Label lblEmail;
        private Label lblPhone;
        private Label lblPassword;
        private Label lblRole;

        private TextBox txtUsername;
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtPassword;

        private ComboBox cboRole;

        private Button btnAddUser;
        private Button btnUpdateUser;
        private Button btnResetPassword;
        private Button btnToggleLock;
        private Button btnReload;

        private DataGridView dgvUsers;
        private DataGridViewTextBoxColumn colUserId;
        private DataGridViewTextBoxColumn colUsername;
        private DataGridViewTextBoxColumn colFullName;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colRole;
        private DataGridViewTextBoxColumn colStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            grpUserList = new GroupBox();
            panelInput = new GroupBox();

            lblUsername = new Label();
            lblFullName = new Label();
            lblEmail = new Label();
            lblPhone = new Label();
            lblPassword = new Label();
            lblRole = new Label();

            txtUsername = new TextBox();
            txtFullName = new TextBox();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            txtPassword = new TextBox();

            cboRole = new ComboBox();

            btnAddUser = new Button();
            btnUpdateUser = new Button();
            btnResetPassword = new Button();
            btnToggleLock = new Button();
            btnReload = new Button();

            dgvUsers = new DataGridView();
            colUserId = new DataGridViewTextBoxColumn();
            colUsername = new DataGridViewTextBoxColumn();
            colFullName = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colRole = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            grpUserList.SuspendLayout();
            panelInput.SuspendLayout();
            SuspendLayout();

            // 
            // panelInput (Right Side Compact Account Card - Dock Right)
            // 
            panelInput.BackColor = Color.White;
            panelInput.Dock = DockStyle.Right;
            panelInput.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            panelInput.ForeColor = Color.FromArgb(30, 41, 59);
            panelInput.Location = new Point(734, 12);
            panelInput.Name = "panelInput";
            panelInput.Size = new Size(300, 636);
            panelInput.TabIndex = 1;
            panelInput.TabStop = false;
            panelInput.Text = "THÔNG TIN & THAO TÁC TÀI KHOẢN";

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblUsername.ForeColor = Color.FromArgb(71, 85, 105);
            lblUsername.Location = new Point(16, 30);
            lblUsername.Text = "Tên đăng nhập (*):";

            txtUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            txtUsername.Location = new Point(16, 50);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Nhập username...";
            txtUsername.Size = new Size(268, 25);

            // lblFullName
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblFullName.ForeColor = Color.FromArgb(71, 85, 105);
            lblFullName.Location = new Point(16, 82);
            lblFullName.Text = "Họ và tên nhân viên:";

            txtFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            txtFullName.Location = new Point(16, 102);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Nhập họ và tên...";
            txtFullName.Size = new Size(268, 25);

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblEmail.ForeColor = Color.FromArgb(71, 85, 105);
            lblEmail.Location = new Point(16, 134);
            lblEmail.Text = "Địa chỉ Email:";

            txtEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            txtEmail.Location = new Point(16, 154);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Nhập email...";
            txtEmail.Size = new Size(268, 25);

            // lblPhone
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblPhone.ForeColor = Color.FromArgb(71, 85, 105);
            lblPhone.Location = new Point(16, 186);
            lblPhone.Text = "Số điện thoại:";

            txtPhone.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            txtPhone.Location = new Point(16, 206);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "Nhập số điện thoại...";
            txtPhone.Size = new Size(268, 25);

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblPassword.ForeColor = Color.FromArgb(71, 85, 105);
            lblPassword.Location = new Point(16, 238);
            lblPassword.Text = "Mật khẩu (khi tạo mới / đặt lại):";

            txtPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            txtPassword.Location = new Point(16, 258);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Nhập mật khẩu...";
            txtPassword.Size = new Size(268, 25);
            txtPassword.UseSystemPasswordChar = true;

            // lblRole
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblRole.ForeColor = Color.FromArgb(71, 85, 105);
            lblRole.Location = new Point(16, 290);
            lblRole.Text = "Vai trò hệ thống (Bảng Roles):";

            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            cboRole.FormattingEnabled = true;
            cboRole.Location = new Point(16, 310);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(268, 26);

            // btnAddUser
            btnAddUser.BackColor = Color.FromArgb(22, 163, 74);
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAddUser.ForeColor = Color.White;
            btnAddUser.Location = new Point(16, 352);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(268, 36);
            btnAddUser.Text = "➕ Thêm tài khoản mới";
            btnAddUser.UseVisualStyleBackColor = false;
            btnAddUser.Cursor = Cursors.Hand;
            btnAddUser.Click += btnAddUser_Click;

            // btnToggleLock
            btnToggleLock.BackColor = Color.FromArgb(217, 119, 6);
            btnToggleLock.FlatAppearance.BorderSize = 0;
            btnToggleLock.FlatStyle = FlatStyle.Flat;
            btnToggleLock.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnToggleLock.ForeColor = Color.White;
            btnToggleLock.Location = new Point(16, 396);
            btnToggleLock.Name = "btnToggleLock";
            btnToggleLock.Size = new Size(268, 36);
            btnToggleLock.Text = "🔒 Khóa / Mở khóa tài khoản";
            btnToggleLock.UseVisualStyleBackColor = false;
            btnToggleLock.Cursor = Cursors.Hand;
            btnToggleLock.Click += btnToggleLock_Click;

            // btnUpdateUser
            btnUpdateUser.BackColor = Color.FromArgb(37, 99, 235);
            btnUpdateUser.FlatAppearance.BorderSize = 0;
            btnUpdateUser.FlatStyle = FlatStyle.Flat;
            btnUpdateUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpdateUser.ForeColor = Color.White;
            btnUpdateUser.Location = new Point(16, 440);
            btnUpdateUser.Name = "btnUpdateUser";
            btnUpdateUser.Size = new Size(130, 36);
            btnUpdateUser.Text = "✏️ Lưu thông tin";
            btnUpdateUser.UseVisualStyleBackColor = false;
            btnUpdateUser.Cursor = Cursors.Hand;
            btnUpdateUser.Click += btnUpdateUser_Click;

            // btnResetPassword
            btnResetPassword.BackColor = Color.FromArgb(14, 116, 144);
            btnResetPassword.FlatAppearance.BorderSize = 0;
            btnResetPassword.FlatStyle = FlatStyle.Flat;
            btnResetPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnResetPassword.ForeColor = Color.White;
            btnResetPassword.Location = new Point(154, 440);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(130, 36);
            btnResetPassword.Text = "🔑 Đặt lại MK";
            btnResetPassword.UseVisualStyleBackColor = false;
            btnResetPassword.Cursor = Cursors.Hand;
            btnResetPassword.Click += btnResetPassword_Click;

            // btnReload
            btnReload.BackColor = Color.FromArgb(100, 116, 139);
            btnReload.FlatAppearance.BorderSize = 0;
            btnReload.FlatStyle = FlatStyle.Flat;
            btnReload.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReload.ForeColor = Color.White;
            btnReload.Location = new Point(16, 484);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(268, 34);
            btnReload.Text = "🔄 Làm mới / Bỏ chọn";
            btnReload.UseVisualStyleBackColor = false;
            btnReload.Cursor = Cursors.Hand;
            btnReload.Click += btnReload_Click;

            panelInput.Controls.Add(lblUsername);
            panelInput.Controls.Add(txtUsername);
            panelInput.Controls.Add(lblFullName);
            panelInput.Controls.Add(txtFullName);
            panelInput.Controls.Add(lblEmail);
            panelInput.Controls.Add(txtEmail);
            panelInput.Controls.Add(lblPhone);
            panelInput.Controls.Add(txtPhone);
            panelInput.Controls.Add(lblPassword);
            panelInput.Controls.Add(txtPassword);
            panelInput.Controls.Add(lblRole);
            panelInput.Controls.Add(cboRole);
            panelInput.Controls.Add(btnAddUser);
            panelInput.Controls.Add(btnToggleLock);
            panelInput.Controls.Add(btnUpdateUser);
            panelInput.Controls.Add(btnResetPassword);
            panelInput.Controls.Add(btnReload);

            // 
            // grpUserList (Center/Left Table - Dock Fill)
            // 
            grpUserList.BackColor = Color.White;
            grpUserList.Controls.Add(dgvUsers);
            grpUserList.Dock = DockStyle.Fill;
            grpUserList.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpUserList.ForeColor = Color.FromArgb(30, 41, 59);
            grpUserList.Location = new Point(16, 12);
            grpUserList.Name = "grpUserList";
            grpUserList.Padding = new Padding(10, 8, 12, 10);
            grpUserList.Size = new Size(718, 636);
            grpUserList.TabIndex = 0;
            grpUserList.TabStop = false;
            grpUserList.Text = "DANH SÁCH TÀI KHOẢN NHÂN VIÊN TRONG HỆ THỐNG";

            // 
            // dgvUsers (Explicitly configured columns - no extra EffectiveId column!)
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AllowUserToResizeRows = false;
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsers.GridColor = Color.FromArgb(226, 232, 240);
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvUsers.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Padding = new Padding(6, 4, 6, 4),
                WrapMode = DataGridViewTriState.False
            };
            dgvUsers.ColumnHeadersHeight = 38;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvUsers.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175),
                Padding = new Padding(6, 2, 6, 2)
            };
            dgvUsers.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252)
            };

            // Định nghĩa cột rõ ràng, vừa vặn, không bị cắt chữ (...) và không bị lộ cột EffectiveId
            colUserId.DataPropertyName = "EffectiveId";
            colUserId.HeaderText = "Mã NV";
            colUserId.Name = "Id";
            colUserId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colUserId.Width = 65;
            colUserId.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter };
            colUserId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colUsername.DataPropertyName = "Username";
            colUsername.HeaderText = "Tên đăng nhập";
            colUsername.Name = "Username";
            colUsername.FillWeight = 18F;
            colUsername.MinimumWidth = 105;

            colFullName.DataPropertyName = "FullName";
            colFullName.HeaderText = "Họ và tên";
            colFullName.Name = "FullName";
            colFullName.FillWeight = 24F;
            colFullName.MinimumWidth = 135;

            colEmail.DataPropertyName = "Email";
            colEmail.HeaderText = "Email";
            colEmail.Name = "Email";
            colEmail.FillWeight = 26F;
            colEmail.MinimumWidth = 150;

            colPhone.DataPropertyName = "Phone";
            colPhone.HeaderText = "Số ĐT";
            colPhone.Name = "Phone";
            colPhone.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPhone.Width = 98;

            colRole.DataPropertyName = "Role";
            colRole.HeaderText = "Vai trò";
            colRole.Name = "Role";
            colRole.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colRole.Width = 90;
            colRole.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter };
            colRole.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colStatus.DataPropertyName = "StatusText";
            colStatus.HeaderText = "Trạng thái";
            colStatus.Name = "StatusText";
            colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStatus.Width = 130;
            colStatus.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft };

            dgvUsers.Columns.AddRange(new DataGridViewColumn[]
            {
                colUserId,
                colUsername,
                colFullName,
                colEmail,
                colPhone,
                colRole,
                colStatus
            });

            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(10, 26);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowTemplate.Height = 34;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(696, 600);
            dgvUsers.CellClick += dgvUsers_CellClick;

            // 
            // FormUserManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = false;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1050, 660);
            Padding = new Padding(12, 10, 12, 10);

            Controls.Add(grpUserList);
            Controls.Add(panelInput);

            FormBorderStyle = FormBorderStyle.None;
            Name = "FormUserManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý tài khoản";

            Load += FormUserManagement_Load;

            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            grpUserList.ResumeLayout(false);
            panelInput.ResumeLayout(false);
            panelInput.PerformLayout();
            ResumeLayout(false);
        }
    }
}
