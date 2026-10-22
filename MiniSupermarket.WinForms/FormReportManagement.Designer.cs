using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormReport
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelTopFilter;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Button btnViewReport;

        private TableLayoutPanel tableKpiCards;
        private Panel panelOrders;
        private Panel panelRevenue;
        private Panel panelBestSeller;

        private Label lblTotalOrdersTitle;
        private Label lblTotalOrdersValue;

        private Label lblTotalRevenueTitle;
        private Label lblTotalRevenueValue;

        private Label lblBestSellerTitle;
        private Label lblBestSellerValue;

        private GroupBox grpRecentOrders;
        private DataGridView dgvRecentOrders;

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

            panelTopFilter = new Panel();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            btnViewReport = new Button();

            tableKpiCards = new TableLayoutPanel();
            panelOrders = new Panel();
            panelRevenue = new Panel();
            panelBestSeller = new Panel();

            lblTotalOrdersTitle = new Label();
            lblTotalOrdersValue = new Label();

            lblTotalRevenueTitle = new Label();
            lblTotalRevenueValue = new Label();

            lblBestSellerTitle = new Label();
            lblBestSellerValue = new Label();

            grpRecentOrders = new GroupBox();
            dgvRecentOrders = new DataGridView();

            panelTopFilter.SuspendLayout();
            tableKpiCards.SuspendLayout();
            panelOrders.SuspendLayout();
            panelRevenue.SuspendLayout();
            panelBestSeller.SuspendLayout();
            grpRecentOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).BeginInit();
            SuspendLayout();

            // 
            // panelTopFilter (Compact Top Filter Bar - Dock Top)
            // 
            panelTopFilter.BackColor = Color.White;
            panelTopFilter.Dock = DockStyle.Top;
            panelTopFilter.Location = new Point(16, 12);
            panelTopFilter.Name = "panelTopFilter";
            panelTopFilter.Size = new Size(1018, 54);
            panelTopFilter.Controls.Add(lblDate);
            panelTopFilter.Controls.Add(dtpDate);
            panelTopFilter.Controls.Add(btnViewReport);

            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(51, 65, 85);
            lblDate.Location = new Point(16, 17);
            lblDate.Name = "lblDate";
            lblDate.Text = "📅 Chọn ngày thống kê:";

            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd/MM/yyyy";
            dtpDate.Font = new Font("Segoe UI", 10F);
            dtpDate.Location = new Point(185, 13);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(140, 27);

            btnViewReport.BackColor = Color.FromArgb(37, 99, 235);
            btnViewReport.FlatAppearance.BorderSize = 0;
            btnViewReport.FlatStyle = FlatStyle.Flat;
            btnViewReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnViewReport.ForeColor = Color.White;
            btnViewReport.Location = new Point(340, 11);
            btnViewReport.Name = "btnViewReport";
            btnViewReport.Size = new Size(145, 32);
            btnViewReport.Text = "📊 Xem báo cáo";
            btnViewReport.UseVisualStyleBackColor = false;
            btnViewReport.Cursor = Cursors.Hand;
            btnViewReport.Click += btnViewReport_Click;

            // 
            // tableKpiCards (3 Equal Responsive Columns - Dock Top)
            // 
            tableKpiCards.ColumnCount = 3;
            tableKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableKpiCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableKpiCards.Dock = DockStyle.Top;
            tableKpiCards.Location = new Point(16, 66);
            tableKpiCards.Name = "tableKpiCards";
            tableKpiCards.Padding = new Padding(0, 12, 0, 12);
            tableKpiCards.RowCount = 1;
            tableKpiCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableKpiCards.Size = new Size(1018, 134);

            // Card 1: panelOrders
            panelOrders.BackColor = Color.White;
            panelOrders.Dock = DockStyle.Fill;
            panelOrders.Margin = new Padding(0, 0, 8, 0);
            panelOrders.Name = "panelOrders";

            lblTotalOrdersTitle.AutoSize = true;
            lblTotalOrdersTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTotalOrdersTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTotalOrdersTitle.Location = new Point(18, 16);
            lblTotalOrdersTitle.Text = "🧾 TỔNG SỐ ĐƠN HÀNG";

            lblTotalOrdersValue.AutoSize = true;
            lblTotalOrdersValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalOrdersValue.ForeColor = Color.FromArgb(37, 99, 235);
            lblTotalOrdersValue.Location = new Point(18, 46);
            lblTotalOrdersValue.Text = "0";

            panelOrders.Controls.Add(lblTotalOrdersTitle);
            panelOrders.Controls.Add(lblTotalOrdersValue);

            // Card 2: panelRevenue
            panelRevenue.BackColor = Color.White;
            panelRevenue.Dock = DockStyle.Fill;
            panelRevenue.Margin = new Padding(4, 0, 4, 0);
            panelRevenue.Name = "panelRevenue";

            lblTotalRevenueTitle.AutoSize = true;
            lblTotalRevenueTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTotalRevenueTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTotalRevenueTitle.Location = new Point(18, 16);
            lblTotalRevenueTitle.Text = "💰 TỔNG DOANH THU";

            lblTotalRevenueValue.AutoSize = true;
            lblTotalRevenueValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalRevenueValue.ForeColor = Color.FromArgb(22, 163, 74);
            lblTotalRevenueValue.Location = new Point(18, 50);
            lblTotalRevenueValue.Text = "0 VNĐ";

            panelRevenue.Controls.Add(lblTotalRevenueTitle);
            panelRevenue.Controls.Add(lblTotalRevenueValue);

            // Card 3: panelBestSeller
            panelBestSeller.BackColor = Color.White;
            panelBestSeller.Dock = DockStyle.Fill;
            panelBestSeller.Margin = new Padding(8, 0, 0, 0);
            panelBestSeller.Name = "panelBestSeller";

            lblBestSellerTitle.AutoSize = true;
            lblBestSellerTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBestSellerTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblBestSellerTitle.Location = new Point(18, 16);
            lblBestSellerTitle.Text = "🔥 SẢN PHẨM BÁN CHẠY NHẤT";

            lblBestSellerValue.AutoSize = true;
            lblBestSellerValue.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBestSellerValue.ForeColor = Color.FromArgb(217, 119, 6);
            lblBestSellerValue.Location = new Point(18, 52);
            lblBestSellerValue.MaximumSize = new Size(310, 50);
            lblBestSellerValue.Text = "Chưa có";

            panelBestSeller.Controls.Add(lblBestSellerTitle);
            panelBestSeller.Controls.Add(lblBestSellerValue);

            tableKpiCards.Controls.Add(panelOrders, 0, 0);
            tableKpiCards.Controls.Add(panelRevenue, 1, 0);
            tableKpiCards.Controls.Add(panelBestSeller, 2, 0);

            // 
            // grpRecentOrders (Full-Height Bottom Table - Dock Fill)
            // 
            grpRecentOrders.BackColor = Color.White;
            grpRecentOrders.Controls.Add(dgvRecentOrders);
            grpRecentOrders.Dock = DockStyle.Fill;
            grpRecentOrders.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpRecentOrders.ForeColor = Color.FromArgb(30, 41, 59);
            grpRecentOrders.Location = new Point(16, 200);
            grpRecentOrders.Name = "grpRecentOrders";
            grpRecentOrders.Padding = new Padding(10, 8, 10, 10);
            grpRecentOrders.Size = new Size(1018, 448);
            grpRecentOrders.TabStop = false;
            grpRecentOrders.Text = "DANH SÁCH ĐƠN HÀNG PHÁT SINH TRONG NGÀY";

            // dgvRecentOrders
            dgvRecentOrders.AllowUserToAddRows = false;
            dgvRecentOrders.AllowUserToDeleteRows = false;
            dgvRecentOrders.AllowUserToResizeRows = false;
            dgvRecentOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecentOrders.BackgroundColor = Color.White;
            dgvRecentOrders.BorderStyle = BorderStyle.None;
            dgvRecentOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecentOrders.GridColor = Color.FromArgb(226, 232, 240);
            dgvRecentOrders.EnableHeadersVisualStyles = false;
            dgvRecentOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRecentOrders.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Padding = new Padding(6)
            };
            dgvRecentOrders.ColumnHeadersHeight = 40;
            dgvRecentOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvRecentOrders.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175),
                Padding = new Padding(6, 2, 6, 2)
            };
            dgvRecentOrders.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252)
            };
            dgvRecentOrders.Dock = DockStyle.Fill;
            dgvRecentOrders.Location = new Point(10, 26);
            dgvRecentOrders.MultiSelect = false;
            dgvRecentOrders.ReadOnly = true;
            dgvRecentOrders.RowHeadersVisible = false;
            dgvRecentOrders.RowTemplate.Height = 36;
            dgvRecentOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentOrders.Size = new Size(998, 412);

            // 
            // FormReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = false;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1050, 660);
            Padding = new Padding(16, 12, 16, 12);

            Controls.Add(grpRecentOrders);
            Controls.Add(tableKpiCards);
            Controls.Add(panelTopFilter);

            FormBorderStyle = FormBorderStyle.None;
            Name = "FormReport";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo cáo doanh thu";

            Load += FormReport_Load;

            ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).EndInit();
            panelTopFilter.ResumeLayout(false);
            panelTopFilter.PerformLayout();
            tableKpiCards.ResumeLayout(false);
            panelOrders.ResumeLayout(false);
            panelOrders.PerformLayout();
            panelRevenue.ResumeLayout(false);
            panelRevenue.PerformLayout();
            panelBestSeller.ResumeLayout(false);
            panelBestSeller.PerformLayout();
            grpRecentOrders.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
