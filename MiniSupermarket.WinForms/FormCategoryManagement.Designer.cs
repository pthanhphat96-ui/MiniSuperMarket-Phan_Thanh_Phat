using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
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
            this.components = new System.ComponentModel.Container();

            this.grpSearch = new GroupBox();
            this.lblKeyword = new Label();
            this.txtKeyword = new TextBox();
            this.btnSearch = new Button();
            this.btnLoad = new Button();

            this.grpCategoryList = new GroupBox();
            this.dgvCategories = new DataGridView();

            this.colCategoryId = new DataGridViewTextBoxColumn();
            this.colCategoryName = new DataGridViewTextBoxColumn();
            this.colDescription = new DataGridViewTextBoxColumn();

            this.grpCategoryInfo = new GroupBox();
            this.lblId = new Label();
            this.txtId = new TextBox();

            this.lblCategoryName = new Label();
            this.txtCategoryName = new TextBox();

            this.lblDescription = new Label();
            this.txtDescription = new TextBox();

            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();

            this.statusStrip1 = new StatusStrip();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new ToolStripStatusLabel();

            this.grpSearch.SuspendLayout();
            this.grpCategoryList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.grpCategoryInfo.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // 
            // grpSearch (Top Search Bar - Dock Top)
            // 
            this.grpSearch.BackColor = Color.White;
            this.grpSearch.Controls.Add(this.lblKeyword);
            this.grpSearch.Controls.Add(this.txtKeyword);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.btnLoad);
            this.grpSearch.Dock = DockStyle.Top;
            this.grpSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpSearch.ForeColor = Color.FromArgb(30, 41, 59);
            this.grpSearch.Location = new Point(16, 12);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new Size(1018, 66);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "TÌM KIẾM & LỌC NHÓM HÀNG";

            // lblKeyword
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.lblKeyword.Location = new Point(16, 29);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Text = "Từ khóa:";

            // txtKeyword (Compact width)
            this.txtKeyword.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtKeyword.Location = new Point(82, 25);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.PlaceholderText = "Nhập tên nhóm hàng...";
            this.txtKeyword.Size = new Size(220, 27);
            this.txtKeyword.TabIndex = 0;

            // btnSearch
            this.btnSearch.BackColor = Color.FromArgb(37, 99, 235);
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Location = new Point(314, 23);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(105, 31);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "🔍 Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Cursor = Cursors.Hand;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // btnLoad
            this.btnLoad.BackColor = Color.FromArgb(100, 116, 139);
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.FlatStyle = FlatStyle.Flat;
            this.btnLoad.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnLoad.ForeColor = Color.White;
            this.btnLoad.Location = new Point(428, 23);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new Size(105, 31);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "🔄 Tải lại";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Cursor = Cursors.Hand;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

            // 
            // grpCategoryInfo (Right Detail Card - Dock Right, Compact Fields)
            // 
            this.grpCategoryInfo.BackColor = Color.White;
            this.grpCategoryInfo.Controls.Add(this.lblId);
            this.grpCategoryInfo.Controls.Add(this.txtId);
            this.grpCategoryInfo.Controls.Add(this.lblCategoryName);
            this.grpCategoryInfo.Controls.Add(this.txtCategoryName);
            this.grpCategoryInfo.Controls.Add(this.lblDescription);
            this.grpCategoryInfo.Controls.Add(this.txtDescription);
            this.grpCategoryInfo.Controls.Add(this.btnAdd);
            this.grpCategoryInfo.Controls.Add(this.btnUpdate);
            this.grpCategoryInfo.Controls.Add(this.btnDelete);
            this.grpCategoryInfo.Dock = DockStyle.Right;
            this.grpCategoryInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpCategoryInfo.ForeColor = Color.FromArgb(30, 41, 59);
            this.grpCategoryInfo.Location = new Point(714, 78);
            this.grpCategoryInfo.Name = "grpCategoryInfo";
            this.grpCategoryInfo.Padding = new Padding(12);
            this.grpCategoryInfo.Size = new Size(320, 548);
            this.grpCategoryInfo.TabIndex = 2;
            this.grpCategoryInfo.TabStop = false;
            this.grpCategoryInfo.Text = "THÔNG TIN NHÓM HÀNG";

            // lblId
            this.lblId.AutoSize = true;
            this.lblId.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.lblId.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblId.Location = new Point(18, 36);
            this.lblId.Name = "lblId";
            this.lblId.Text = "Mã nhóm hàng (ID):";

            // txtId
            this.txtId.BackColor = Color.FromArgb(241, 245, 249);
            this.txtId.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtId.Location = new Point(18, 60);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new Size(284, 27);
            this.txtId.TabIndex = 1;

            // lblCategoryName
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.lblCategoryName.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblCategoryName.Location = new Point(18, 102);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Text = "Tên nhóm hàng (*):";

            // txtCategoryName
            this.txtCategoryName.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtCategoryName.Location = new Point(18, 126);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.PlaceholderText = "Nhập tên nhóm hàng...";
            this.txtCategoryName.Size = new Size(284, 27);
            this.txtCategoryName.TabIndex = 3;

            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.lblDescription.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblDescription.Location = new Point(18, 168);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Text = "Mô tả chi tiết:";

            // txtDescription
            this.txtDescription.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtDescription.Location = new Point(18, 192);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.txtDescription.Size = new Size(284, 80);
            this.txtDescription.TabIndex = 5;

            // btnAdd
            this.btnAdd.BackColor = Color.FromArgb(22, 163, 74);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Location = new Point(18, 294);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(284, 38);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "➕ Thêm nhóm hàng mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Cursor = Cursors.Hand;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.BackColor = Color.FromArgb(37, 99, 235);
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnUpdate.ForeColor = Color.White;
            this.btnUpdate.Location = new Point(18, 342);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(137, 38);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "✏️ Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Cursor = Cursors.Hand;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.BackColor = Color.FromArgb(220, 38, 38);
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Location = new Point(165, 342);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(137, 38);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "🗑️ Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Cursor = Cursors.Hand;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // grpCategoryList (Center/Left Table - Dock Fill)
            // 
            this.grpCategoryList.BackColor = Color.White;
            this.grpCategoryList.Controls.Add(this.dgvCategories);
            this.grpCategoryList.Dock = DockStyle.Fill;
            this.grpCategoryList.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpCategoryList.ForeColor = Color.FromArgb(30, 41, 59);
            this.grpCategoryList.Location = new Point(16, 78);
            this.grpCategoryList.Name = "grpCategoryList";
            this.grpCategoryList.Padding = new Padding(10, 8, 10, 10);
            this.grpCategoryList.Size = new Size(698, 548);
            this.grpCategoryList.TabIndex = 1;
            this.grpCategoryList.TabStop = false;
            this.grpCategoryList.Text = "DANH SÁCH NHÓM HÀNG TRONG HỆ THỐNG";

            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AllowUserToDeleteRows = false;
            this.dgvCategories.AllowUserToResizeRows = false;
            this.dgvCategories.AutoGenerateColumns = false;
            this.dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.BackgroundColor = Color.White;
            this.dgvCategories.BorderStyle = BorderStyle.None;
            this.dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCategories.GridColor = Color.FromArgb(226, 232, 240);
            this.dgvCategories.EnableHeadersVisualStyles = false;
            this.dgvCategories.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvCategories.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(6)
            };
            this.dgvCategories.ColumnHeadersHeight = 40;
            this.dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCategories.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175),
                Padding = new Padding(6, 2, 6, 2)
            };
            this.dgvCategories.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252)
            };
            this.dgvCategories.Columns.AddRange(new DataGridViewColumn[]
            {
                this.colCategoryId,
                this.colCategoryName,
                this.colDescription
            });
            this.dgvCategories.Dock = DockStyle.Fill;
            this.dgvCategories.Location = new Point(10, 28);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.RowTemplate.Height = 36;
            this.dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new Size(678, 510);
            this.dgvCategories.TabIndex = 0;
            this.dgvCategories.CellClick += new DataGridViewCellEventHandler(this.dgvCategories_CellClick);

            // colCategoryId
            this.colCategoryId.DataPropertyName = "CategoryId";
            this.colCategoryId.HeaderText = "Mã ID";
            this.colCategoryId.Name = "CategoryId";
            this.colCategoryId.ReadOnly = true;
            this.colCategoryId.FillWeight = 15F;

            // colCategoryName
            this.colCategoryName.DataPropertyName = "CategoryName";
            this.colCategoryName.HeaderText = "Tên Nhóm Hàng";
            this.colCategoryName.Name = "CategoryName";
            this.colCategoryName.ReadOnly = true;
            this.colCategoryName.FillWeight = 38F;

            // colDescription
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Mô Tả Chi Tiết";
            this.colDescription.Name = "Description";
            this.colDescription.ReadOnly = true;
            this.colDescription.FillWeight = 47F;

            // statusStrip1
            this.statusStrip1.BackColor = Color.White;
            this.statusStrip1.Items.AddRange(new ToolStripItem[]
            {
                this.toolStripStatusLabel1,
                this.toolStripStatusLabel2
            });
            this.statusStrip1.Location = new Point(16, 626);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(1018, 22);
            this.statusStrip1.TabIndex = 3;

            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Text = "Sẵn sàng";

            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Text = "   |   Kết nối CSDL: Trực tuyến";

            // 
            // FormCategoryManagement
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.ClientSize = new Size(1050, 660);
            this.Padding = new Padding(16, 12, 16, 12);

            // Thứ tự Add để Dock chuẩn: Fill trước, Right, Bottom, Top sau
            this.Controls.Add(this.grpCategoryList);
            this.Controls.Add(this.grpCategoryInfo);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.statusStrip1);

            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FormCategoryManagement";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý Danh mục Nhóm hàng";

            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpCategoryList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.grpCategoryInfo.ResumeLayout(false);
            this.grpCategoryInfo.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private GroupBox grpSearch;
        private GroupBox grpCategoryList;
        private GroupBox grpCategoryInfo;

        private Label lblKeyword;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;

        private DataGridView dgvCategories;
        private DataGridViewTextBoxColumn colCategoryId;
        private DataGridViewTextBoxColumn colCategoryName;
        private DataGridViewTextBoxColumn colDescription;

        private Label lblId;
        private TextBox txtId;
        private Label lblCategoryName;
        private TextBox txtCategoryName;
        private Label lblDescription;
        private TextBox txtDescription;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
    }
}
