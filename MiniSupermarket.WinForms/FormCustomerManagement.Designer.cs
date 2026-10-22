using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvCustomers;

        private TextBox txtCustomerId;
        private TextBox txtCustomerName;
        private TextBox txtPhoneNumber;
        private TextBox txtAddress;
        private TextBox txtRewardPoints;
        private TextBox txtMembershipRank;

        private Label lblCustomerId;
        private Label lblCustomerName;
        private Label lblPhoneNumber;
        private Label lblAddress;
        private Label lblRewardPoints;
        private Label lblMembershipRank;

        private Button btnLoad;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnSearch;

        private Panel panelPaging;
        private Button btnPrev;
        private Label lblPaging;
        private Button btnNext;

        private Panel panelTopBar;
        private GroupBox grpCustomerInfo;
        private GroupBox grpCustomerList;

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
            this.dgvCustomers = new DataGridView();

            this.txtCustomerId = new TextBox();
            this.txtCustomerName = new TextBox();
            this.txtPhoneNumber = new TextBox();
            this.txtAddress = new TextBox();
            this.txtRewardPoints = new TextBox();
            this.txtMembershipRank = new TextBox();

            this.lblCustomerId = new Label();
            this.lblCustomerName = new Label();
            this.lblPhoneNumber = new Label();
            this.lblAddress = new Label();
            this.lblRewardPoints = new Label();
            this.lblMembershipRank = new Label();

            this.btnLoad = new Button();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnSearch = new Button();

            this.panelPaging = new Panel();
            this.btnPrev = new Button();
            this.lblPaging = new Label();
            this.btnNext = new Button();

            this.panelTopBar = new Panel();
            this.grpCustomerInfo = new GroupBox();
            this.grpCustomerList = new GroupBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.panelTopBar.SuspendLayout();
            this.panelPaging.SuspendLayout();
            this.grpCustomerInfo.SuspendLayout();
            this.grpCustomerList.SuspendLayout();
            this.SuspendLayout();

            // 
            // grpCustomerInfo (Right Detail Panel - Dock Right, Compact Fields)
            // 
            this.grpCustomerInfo.BackColor = Color.White;
            this.grpCustomerInfo.Controls.Add(this.lblCustomerId);
            this.grpCustomerInfo.Controls.Add(this.txtCustomerId);
            this.grpCustomerInfo.Controls.Add(this.lblCustomerName);
            this.grpCustomerInfo.Controls.Add(this.txtCustomerName);
            this.grpCustomerInfo.Controls.Add(this.lblPhoneNumber);
            this.grpCustomerInfo.Controls.Add(this.txtPhoneNumber);
            this.grpCustomerInfo.Controls.Add(this.lblAddress);
            this.grpCustomerInfo.Controls.Add(this.txtAddress);
            this.grpCustomerInfo.Controls.Add(this.lblRewardPoints);
            this.grpCustomerInfo.Controls.Add(this.txtRewardPoints);
            this.grpCustomerInfo.Controls.Add(this.lblMembershipRank);
            this.grpCustomerInfo.Controls.Add(this.txtMembershipRank);
            this.grpCustomerInfo.Controls.Add(this.btnAdd);
            this.grpCustomerInfo.Controls.Add(this.btnUpdate);
            this.grpCustomerInfo.Controls.Add(this.btnDelete);
            this.grpCustomerInfo.Controls.Add(this.btnSearch);
            this.grpCustomerInfo.Controls.Add(this.btnLoad);
            this.grpCustomerInfo.Dock = DockStyle.Right;
            this.grpCustomerInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpCustomerInfo.ForeColor = Color.FromArgb(30, 41, 59);
            this.grpCustomerInfo.Location = new Point(714, 12);
            this.grpCustomerInfo.Name = "grpCustomerInfo";
            this.grpCustomerInfo.Size = new Size(320, 636);
            this.grpCustomerInfo.TabIndex = 1;
            this.grpCustomerInfo.TabStop = false;
            this.grpCustomerInfo.Text = "THÔNG TIN KHÁCH HÀNG";

            // lblCustomerId
            this.lblCustomerId.AutoSize = true;
            this.lblCustomerId.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblCustomerId.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblCustomerId.Location = new Point(18, 30);
            this.lblCustomerId.Name = "lblCustomerId";
            this.lblCustomerId.Text = "Mã khách hàng:";

            // txtCustomerId (Compact)
            this.txtCustomerId.BackColor = Color.FromArgb(241, 245, 249);
            this.txtCustomerId.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtCustomerId.Location = new Point(18, 52);
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.ReadOnly = true;
            this.txtCustomerId.Size = new Size(284, 27);

            // lblCustomerName
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblCustomerName.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblCustomerName.Location = new Point(18, 88);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Text = "Tên khách hàng (*):";

            // txtCustomerName
            this.txtCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtCustomerName.Location = new Point(18, 110);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.PlaceholderText = "Nhập họ tên khách hàng...";
            this.txtCustomerName.Size = new Size(284, 27);

            // lblPhoneNumber
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblPhoneNumber.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblPhoneNumber.Location = new Point(18, 146);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Text = "Số điện thoại (*):";

            // txtPhoneNumber
            this.txtPhoneNumber.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtPhoneNumber.Location = new Point(18, 168);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.PlaceholderText = "Nhập SĐT (tối đa 15 số)...";
            this.txtPhoneNumber.Size = new Size(284, 27);

            // lblAddress
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblAddress.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblAddress.Location = new Point(18, 204);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Text = "Địa chỉ liên hệ:";

            // txtAddress
            this.txtAddress.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtAddress.Location = new Point(18, 226);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PlaceholderText = "Nhập địa chỉ...";
            this.txtAddress.Size = new Size(284, 27);

            // lblRewardPoints (Side-by-side compact with Rank)
            this.lblRewardPoints.AutoSize = true;
            this.lblRewardPoints.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblRewardPoints.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblRewardPoints.Location = new Point(18, 262);
            this.lblRewardPoints.Name = "lblRewardPoints";
            this.lblRewardPoints.Text = "Điểm tích lũy:";

            this.txtRewardPoints.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtRewardPoints.Location = new Point(18, 284);
            this.txtRewardPoints.Name = "txtRewardPoints";
            this.txtRewardPoints.Size = new Size(134, 27);
            this.txtRewardPoints.Text = "0";

            // lblMembershipRank
            this.lblMembershipRank.AutoSize = true;
            this.lblMembershipRank.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblMembershipRank.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblMembershipRank.Location = new Point(168, 262);
            this.lblMembershipRank.Name = "lblMembershipRank";
            this.lblMembershipRank.Text = "Hạng thẻ:";

            this.txtMembershipRank.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtMembershipRank.Location = new Point(168, 284);
            this.txtMembershipRank.Name = "txtMembershipRank";
            this.txtMembershipRank.Size = new Size(134, 27);
            this.txtMembershipRank.Text = "Chuẩn";

            // Buttons
            this.btnAdd.BackColor = Color.FromArgb(22, 163, 74);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Location = new Point(18, 330);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(284, 38);
            this.btnAdd.Text = "➕ Thêm khách hàng mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Cursor = Cursors.Hand;

            this.btnUpdate.BackColor = Color.FromArgb(37, 99, 235);
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnUpdate.ForeColor = Color.White;
            this.btnUpdate.Location = new Point(18, 378);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(137, 38);
            this.btnUpdate.Text = "✏️ Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Cursor = Cursors.Hand;

            this.btnDelete.BackColor = Color.FromArgb(220, 38, 38);
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Location = new Point(165, 378);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(137, 38);
            this.btnDelete.Text = "🗑️ Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Cursor = Cursors.Hand;

            this.btnSearch.BackColor = Color.FromArgb(14, 116, 144);
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Location = new Point(18, 426);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(137, 38);
            this.btnSearch.Text = "🔍 Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Cursor = Cursors.Hand;

            this.btnLoad.BackColor = Color.FromArgb(100, 116, 139);
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.FlatStyle = FlatStyle.Flat;
            this.btnLoad.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnLoad.ForeColor = Color.White;
            this.btnLoad.Location = new Point(165, 426);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new Size(137, 38);
            this.btnLoad.Text = "🔄 Tải danh sách";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Cursor = Cursors.Hand;

            // 
            // panelPaging (Thanh điều hướng Phân trang Khách hàng - Bài tập 6.1)
            // 
            this.panelPaging.BackColor = Color.White;
            this.panelPaging.Controls.Add(this.btnNext);
            this.panelPaging.Controls.Add(this.lblPaging);
            this.panelPaging.Controls.Add(this.btnPrev);
            this.panelPaging.Dock = DockStyle.Bottom;
            this.panelPaging.Location = new Point(10, 578);
            this.panelPaging.Name = "panelPaging";
            this.panelPaging.Size = new Size(678, 48);

            // btnPrev
            this.btnPrev.BackColor = Color.FromArgb(51, 65, 85);
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = FlatStyle.Flat;
            this.btnPrev.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnPrev.ForeColor = Color.White;
            this.btnPrev.Location = new Point(6, 8);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new Size(135, 32);
            this.btnPrev.Text = "<< Trang trước";
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Cursor = Cursors.Hand;

            // lblPaging
            this.lblPaging.AutoSize = false;
            this.lblPaging.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblPaging.ForeColor = Color.FromArgb(30, 64, 175);
            this.lblPaging.Location = new Point(150, 8);
            this.lblPaging.Name = "lblPaging";
            this.lblPaging.Size = new Size(270, 32);
            this.lblPaging.Text = "Trang 1 / 1 (Tổng: 0 KH)";
            this.lblPaging.TextAlign = ContentAlignment.MiddleCenter;

            // btnNext
            this.btnNext.BackColor = Color.FromArgb(37, 99, 235);
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = FlatStyle.Flat;
            this.btnNext.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnNext.ForeColor = Color.White;
            this.btnNext.Location = new Point(428, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(135, 32);
            this.btnNext.Text = "Trang sau >>";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Cursor = Cursors.Hand;

            // 
            // grpCustomerList (Center/Left Full-Height Table - Dock Fill)
            // 
            this.grpCustomerList.BackColor = Color.White;
            this.grpCustomerList.Controls.Add(this.dgvCustomers);
            this.grpCustomerList.Controls.Add(this.panelPaging);
            this.grpCustomerList.Dock = DockStyle.Fill;
            this.grpCustomerList.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpCustomerList.ForeColor = Color.FromArgb(30, 41, 59);
            this.grpCustomerList.Location = new Point(16, 12);
            this.grpCustomerList.Name = "grpCustomerList";
            this.grpCustomerList.Padding = new Padding(10, 8, 10, 10);
            this.grpCustomerList.Size = new Size(698, 636);
            this.grpCustomerList.TabIndex = 0;
            this.grpCustomerList.TabStop = false;
            this.grpCustomerList.Text = "DANH SÁCH KHÁCH HÀNG THÂN THIẾT (SẮP XẾP THEO ĐIỂM TÍCH LŨY)";

            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.AllowUserToResizeRows = false;
            this.dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.BackgroundColor = Color.White;
            this.dgvCustomers.BorderStyle = BorderStyle.None;
            this.dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCustomers.GridColor = Color.FromArgb(226, 232, 240);
            this.dgvCustomers.EnableHeadersVisualStyles = false;
            this.dgvCustomers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvCustomers.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Padding = new Padding(6)
            };
            this.dgvCustomers.ColumnHeadersHeight = 40;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCustomers.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175),
                Padding = new Padding(6, 2, 6, 2)
            };
            this.dgvCustomers.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252)
            };
            this.dgvCustomers.Dock = DockStyle.Fill;
            this.dgvCustomers.Location = new Point(10, 26);
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.RowTemplate.Height = 36;
            this.dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.Size = new Size(678, 552);
            this.dgvCustomers.TabIndex = 0;

            // 
            // FormCustomerManagement
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.ClientSize = new Size(1050, 660);
            this.Padding = new Padding(16, 12, 16, 12);

            this.Controls.Add(this.grpCustomerList);
            this.Controls.Add(this.grpCustomerInfo);

            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FormCustomerManagement";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý khách hàng";

            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.panelTopBar.ResumeLayout(false);
            this.panelPaging.ResumeLayout(false);
            this.grpCustomerInfo.ResumeLayout(false);
            this.grpCustomerInfo.PerformLayout();
            this.grpCustomerList.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
