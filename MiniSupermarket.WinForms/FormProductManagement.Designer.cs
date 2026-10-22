using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
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
            this.panelTop = new Panel();
            this.lblSearchBarcode = new Label();
            this.txtSearchBarcode = new TextBox();
            this.lblFilterCategory = new Label();
            this.cboFilterCategory = new ComboBox();
            this.btnSearch = new Button();
            this.btnLoad = new Button();

            this.panelGridContainer = new Panel();
            this.dgvProducts = new DataGridView();
            this.colProductId = new DataGridViewTextBoxColumn();
            this.colBarcode = new DataGridViewTextBoxColumn();
            this.colProductName = new DataGridViewTextBoxColumn();
            this.colCategoryName = new DataGridViewTextBoxColumn();
            this.colPrice = new DataGridViewTextBoxColumn();
            this.colStock = new DataGridViewTextBoxColumn();
            this.colCategoryId = new DataGridViewTextBoxColumn();

            this.groupBoxDetails = new GroupBox();
            this.lblId = new Label();
            this.txtId = new TextBox();
            this.lblBarcode = new Label();
            this.txtBarcode = new TextBox();
            this.lblProductName = new Label();
            this.txtProductName = new TextBox();
            this.lblCategory = new Label();
            this.cboCategory = new ComboBox();
            this.lblPrice = new Label();
            this.nudPrice = new NumericUpDown();
            this.lblStock = new Label();
            this.nudStock = new NumericUpDown();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();

            this.panelTop.SuspendLayout();
            this.panelGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.groupBoxDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).BeginInit();
            this.SuspendLayout();

            // 
            // panelTop (Top Filter Bar - Dock Top)
            // 
            this.panelTop.BackColor = Color.White;
            this.panelTop.Controls.Add(this.btnLoad);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.cboFilterCategory);
            this.panelTop.Controls.Add(this.lblFilterCategory);
            this.panelTop.Controls.Add(this.txtSearchBarcode);
            this.panelTop.Controls.Add(this.lblSearchBarcode);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(16, 12);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new Size(1018, 56);
            this.panelTop.TabIndex = 0;

            // lblSearchBarcode
            this.lblSearchBarcode.AutoSize = true;
            this.lblSearchBarcode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblSearchBarcode.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblSearchBarcode.Location = new Point(16, 18);
            this.lblSearchBarcode.Name = "lblSearchBarcode";
            this.lblSearchBarcode.Text = "Từ khóa / Mã vạch:";

            // txtSearchBarcode (Compact width)
            this.txtSearchBarcode.Font = new Font("Segoe UI", 10F);
            this.txtSearchBarcode.Location = new Point(148, 14);
            this.txtSearchBarcode.Name = "txtSearchBarcode";
            this.txtSearchBarcode.PlaceholderText = "Nhập mã vạch hoặc tên SP...";
            this.txtSearchBarcode.Size = new Size(195, 27);
            this.txtSearchBarcode.TabIndex = 1;

            // lblFilterCategory
            this.lblFilterCategory.AutoSize = true;
            this.lblFilterCategory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblFilterCategory.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblFilterCategory.Location = new Point(362, 18);
            this.lblFilterCategory.Name = "lblFilterCategory";
            this.lblFilterCategory.Text = "Lọc nhóm hàng:";

            // cboFilterCategory (Compact width)
            this.cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboFilterCategory.Font = new Font("Segoe UI", 10F);
            this.cboFilterCategory.FormattingEnabled = true;
            this.cboFilterCategory.Location = new Point(476, 14);
            this.cboFilterCategory.Name = "cboFilterCategory";
            this.cboFilterCategory.Size = new Size(210, 28);
            this.cboFilterCategory.TabIndex = 2;
            this.cboFilterCategory.SelectedIndexChanged += new System.EventHandler(this.cboFilterCategory_SelectedIndexChanged);

            // btnSearch
            this.btnSearch.BackColor = Color.FromArgb(37, 99, 235);
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Location = new Point(700, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(105, 32);
            this.btnSearch.TabIndex = 3;
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
            this.btnLoad.Location = new Point(814, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new Size(105, 32);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "🔄 Làm mới";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Cursor = Cursors.Hand;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

            // 
            // groupBoxDetails (Right Side Compact Detail Card - Dock Right)
            // 
            this.groupBoxDetails.BackColor = Color.White;
            this.groupBoxDetails.Controls.Add(this.btnDelete);
            this.groupBoxDetails.Controls.Add(this.btnUpdate);
            this.groupBoxDetails.Controls.Add(this.btnAdd);
            this.groupBoxDetails.Controls.Add(this.cboCategory);
            this.groupBoxDetails.Controls.Add(this.lblCategory);
            this.groupBoxDetails.Controls.Add(this.nudStock);
            this.groupBoxDetails.Controls.Add(this.lblStock);
            this.groupBoxDetails.Controls.Add(this.nudPrice);
            this.groupBoxDetails.Controls.Add(this.lblPrice);
            this.groupBoxDetails.Controls.Add(this.txtProductName);
            this.groupBoxDetails.Controls.Add(this.lblProductName);
            this.groupBoxDetails.Controls.Add(this.txtBarcode);
            this.groupBoxDetails.Controls.Add(this.lblBarcode);
            this.groupBoxDetails.Controls.Add(this.txtId);
            this.groupBoxDetails.Controls.Add(this.lblId);
            this.groupBoxDetails.Dock = DockStyle.Right;
            this.groupBoxDetails.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.groupBoxDetails.ForeColor = Color.FromArgb(30, 41, 59);
            this.groupBoxDetails.Location = new Point(724, 68);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new Size(310, 580);
            this.groupBoxDetails.TabIndex = 2;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "THÔNG TIN SẢN PHẨM";

            // lblId
            this.lblId.AutoSize = true;
            this.lblId.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblId.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblId.Location = new Point(16, 32);
            this.lblId.Name = "lblId";
            this.lblId.Text = "Mã sản phẩm (ID):";

            // txtId
            this.txtId.BackColor = Color.FromArgb(241, 245, 249);
            this.txtId.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtId.Location = new Point(16, 54);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new Size(278, 27);
            this.txtId.TabIndex = 1;

            // lblBarcode
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblBarcode.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblBarcode.Location = new Point(16, 90);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Text = "Mã vạch (Barcode):";

            // txtBarcode
            this.txtBarcode.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtBarcode.Location = new Point(16, 112);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.PlaceholderText = "Nhập mã vạch...";
            this.txtBarcode.Size = new Size(278, 27);
            this.txtBarcode.TabIndex = 2;

            // lblProductName
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblProductName.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblProductName.Location = new Point(16, 148);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Text = "Tên sản phẩm:";

            // txtProductName
            this.txtProductName.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtProductName.Location = new Point(16, 170);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.PlaceholderText = "Nhập tên sản phẩm...";
            this.txtProductName.Size = new Size(278, 27);
            this.txtProductName.TabIndex = 3;

            // lblCategory
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblCategory.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblCategory.Location = new Point(16, 206);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Text = "Nhóm hàng (Danh mục):";

            // cboCategory
            this.cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new Point(16, 228);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new Size(278, 28);
            this.cboCategory.TabIndex = 4;

            // lblPrice
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblPrice.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblPrice.Location = new Point(16, 266);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Text = "Đơn giá bán (VNĐ):";

            // nudPrice (Compact side-by-side with Stock)
            this.nudPrice.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.nudPrice.Location = new Point(16, 288);
            this.nudPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new Size(165, 27);
            this.nudPrice.TabIndex = 5;
            this.nudPrice.ThousandsSeparator = true;

            // lblStock
            this.lblStock.AutoSize = true;
            this.lblStock.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblStock.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblStock.Location = new Point(192, 266);
            this.lblStock.Name = "lblStock";
            this.lblStock.Text = "Tồn kho:";

            // nudStock
            this.nudStock.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.nudStock.Location = new Point(192, 288);
            this.nudStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudStock.Name = "nudStock";
            this.nudStock.Size = new Size(102, 27);
            this.nudStock.TabIndex = 6;

            // btnAdd
            this.btnAdd.BackColor = Color.FromArgb(22, 163, 74);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Location = new Point(16, 336);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(278, 38);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "➕ Thêm sản phẩm mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Cursor = Cursors.Hand;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.BackColor = Color.FromArgb(37, 99, 235);
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnUpdate.ForeColor = Color.White;
            this.btnUpdate.Location = new Point(16, 384);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(134, 38);
            this.btnUpdate.TabIndex = 8;
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
            this.btnDelete.Location = new Point(160, 384);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(134, 38);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "🗑️ Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Cursor = Cursors.Hand;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // panelGridContainer (Center Table Container - Dock Fill)
            // 
            this.panelGridContainer.BackColor = Color.FromArgb(241, 245, 249);
            this.panelGridContainer.Controls.Add(this.dgvProducts);
            this.panelGridContainer.Dock = DockStyle.Fill;
            this.panelGridContainer.Location = new Point(16, 68);
            this.panelGridContainer.Name = "panelGridContainer";
            this.panelGridContainer.Padding = new Padding(0, 10, 12, 0);
            this.panelGridContainer.Size = new Size(708, 580);
            this.panelGridContainer.TabIndex = 1;

            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            this.dgvProducts.BackgroundColor = Color.White;
            this.dgvProducts.BorderStyle = BorderStyle.None;
            this.dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProducts.GridColor = Color.FromArgb(226, 232, 240);
            this.dgvProducts.EnableHeadersVisualStyles = false;
            this.dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvProducts.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Padding = new Padding(6)
            };
            this.dgvProducts.ColumnHeadersHeight = 40;
            this.dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProducts.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175),
                Padding = new Padding(6, 2, 6, 2)
            };
            this.dgvProducts.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252)
            };
            this.dgvProducts.Columns.AddRange(new DataGridViewColumn[] {
                this.colProductId,
                this.colBarcode,
                this.colProductName,
                this.colCategoryName,
                this.colPrice,
                this.colStock,
                this.colCategoryId
            });
            this.dgvProducts.Dock = DockStyle.Fill;
            this.dgvProducts.Location = new Point(0, 10);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowTemplate.Height = 36;
            this.dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new Size(696, 570);
            this.dgvProducts.TabIndex = 0;
            this.dgvProducts.CellClick += new DataGridViewCellEventHandler(this.dgvProducts_CellClick);

            // colProductId
            this.colProductId.DataPropertyName = "ProductId";
            this.colProductId.HeaderText = "Mã SP";
            this.colProductId.Name = "ProductId";
            this.colProductId.ReadOnly = true;
            this.colProductId.Width = 65;

            // colBarcode
            this.colBarcode.DataPropertyName = "Barcode";
            this.colBarcode.HeaderText = "Mã Vạch";
            this.colBarcode.Name = "Barcode";
            this.colBarcode.ReadOnly = true;
            this.colBarcode.Width = 120;

            // colProductName
            this.colProductName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.colProductName.DataPropertyName = "ProductName";
            this.colProductName.HeaderText = "Tên Sản Phẩm";
            this.colProductName.Name = "ProductName";
            this.colProductName.ReadOnly = true;

            // colCategoryName (Hiển thị rõ Tên Nhóm Hàng trên bảng)
            this.colCategoryName.DataPropertyName = "CategoryName";
            this.colCategoryName.HeaderText = "Nhóm Hàng";
            this.colCategoryName.Name = "CategoryName";
            this.colCategoryName.ReadOnly = true;
            this.colCategoryName.Width = 165;

            // colPrice
            this.colPrice.DataPropertyName = "Price";
            this.colPrice.HeaderText = "Đơn Giá";
            this.colPrice.Name = "Price";
            this.colPrice.ReadOnly = true;
            this.colPrice.Width = 115;
            this.colPrice.DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };

            // colStock
            this.colStock.DataPropertyName = "StockQuantity";
            this.colStock.HeaderText = "Tồn Kho";
            this.colStock.Name = "StockQuantity";
            this.colStock.ReadOnly = true;
            this.colStock.Width = 85;
            this.colStock.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter };

            // colCategoryId (Ẩn, dùng để đồng bộ khi chọn dòng)
            this.colCategoryId.DataPropertyName = "CategoryId";
            this.colCategoryId.HeaderText = "Nhóm ID";
            this.colCategoryId.Name = "CategoryId";
            this.colCategoryId.ReadOnly = true;
            this.colCategoryId.Visible = false;

            // 
            // FormProductManagement
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.ClientSize = new Size(1050, 660);
            this.Padding = new Padding(16, 12, 16, 12);

            this.Controls.Add(this.panelGridContainer);
            this.Controls.Add(this.groupBoxDetails);
            this.Controls.Add(this.panelTop);

            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FormProductManagement";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý Sản phẩm";
            this.Load += new System.EventHandler(this.FormProductManagement_Load);

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblSearchBarcode;
        private TextBox txtSearchBarcode;
        private Label lblFilterCategory;
        private ComboBox cboFilterCategory;
        private Button btnSearch;
        private Button btnLoad;

        private Panel panelGridContainer;
        private DataGridView dgvProducts;
        private DataGridViewTextBoxColumn colProductId;
        private DataGridViewTextBoxColumn colBarcode;
        private DataGridViewTextBoxColumn colProductName;
        private DataGridViewTextBoxColumn colCategoryName;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colStock;
        private DataGridViewTextBoxColumn colCategoryId;

        private GroupBox groupBoxDetails;
        private Label lblId;
        private TextBox txtId;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblProductName;
        private TextBox txtProductName;
        private Label lblCategory;
        private ComboBox cboCategory;
        private Label lblPrice;
        private NumericUpDown nudPrice;
        private Label lblStock;
        private NumericUpDown nudStock;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}
