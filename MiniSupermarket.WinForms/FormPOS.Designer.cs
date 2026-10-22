using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormPOS
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
            this.panelLeft = new Panel();
            this.dgvCart = new DataGridView();
            this.panelBarcode = new Panel();
            this.lblBarcode = new Label();
            this.txtBarcode = new TextBox();

            this.panelRight = new Panel();
            this.groupBoxCustomer = new GroupBox();
            this.lblCustomerName = new Label();
            this.label2 = new Label();
            this.txtCustomerPhone = new TextBox();
            this.label1 = new Label();

            this.groupBoxPayment = new GroupBox();
            this.lblChange = new Label();
            this.label6 = new Label();
            this.txtCashReceived = new TextBox();
            this.label5 = new Label();
            this.lblTotalAmount = new Label();
            this.label3 = new Label();

            this.panelActions = new Panel();
            this.btnCheckout = new Button();
            this.btnClearCart = new Button();

            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.panelBarcode.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.groupBoxCustomer.SuspendLayout();
            this.groupBoxPayment.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelRight (Right Payment & Customer Sidebar - Dock Right)
            // 
            this.panelRight.BackColor = Color.FromArgb(241, 245, 249);
            this.panelRight.Controls.Add(this.groupBoxPayment);
            this.panelRight.Controls.Add(this.groupBoxCustomer);
            this.panelRight.Controls.Add(this.panelActions);
            this.panelRight.Dock = DockStyle.Right;
            this.panelRight.Location = new Point(694, 12);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new Padding(12, 0, 0, 0);
            this.panelRight.Size = new Size(340, 636);
            this.panelRight.TabIndex = 1;

            // 
            // groupBoxCustomer (Dock Top in panelRight)
            // 
            this.groupBoxCustomer.BackColor = Color.White;
            this.groupBoxCustomer.Controls.Add(this.lblCustomerName);
            this.groupBoxCustomer.Controls.Add(this.label2);
            this.groupBoxCustomer.Controls.Add(this.txtCustomerPhone);
            this.groupBoxCustomer.Controls.Add(this.label1);
            this.groupBoxCustomer.Dock = DockStyle.Top;
            this.groupBoxCustomer.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.groupBoxCustomer.ForeColor = Color.FromArgb(30, 41, 59);
            this.groupBoxCustomer.Location = new Point(12, 0);
            this.groupBoxCustomer.Name = "groupBoxCustomer";
            this.groupBoxCustomer.Size = new Size(328, 130);
            this.groupBoxCustomer.TabIndex = 0;
            this.groupBoxCustomer.TabStop = false;
            this.groupBoxCustomer.Text = "KHÁCH HÀNG THÂN THIẾT";

            // label1 (SĐT)
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.label1.ForeColor = Color.FromArgb(71, 85, 105);
            this.label1.Location = new Point(16, 36);
            this.label1.Name = "label1";
            this.label1.Text = "SĐT khách:";

            // txtCustomerPhone (Compact)
            this.txtCustomerPhone.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            this.txtCustomerPhone.Location = new Point(105, 32);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.PlaceholderText = "Nhập SĐT + Enter...";
            this.txtCustomerPhone.Size = new Size(205, 29);
            this.txtCustomerPhone.TabIndex = 0;

            // label2 (Tên khách)
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.label2.ForeColor = Color.FromArgb(71, 85, 105);
            this.label2.Location = new Point(16, 82);
            this.label2.Name = "label2";
            this.label2.Text = "Khách hàng:";

            // lblCustomerName
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCustomerName.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblCustomerName.Location = new Point(105, 81);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Text = "Khách vãng lai";

            // 
            // panelActions (Dock Bottom in panelRight)
            // 
            this.panelActions.Controls.Add(this.btnCheckout);
            this.panelActions.Controls.Add(this.btnClearCart);
            this.panelActions.Dock = DockStyle.Bottom;
            this.panelActions.Location = new Point(12, 486);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new Padding(0, 10, 0, 0);
            this.panelActions.Size = new Size(328, 150);
            this.panelActions.TabIndex = 2;

            // btnCheckout
            this.btnCheckout.BackColor = Color.FromArgb(22, 163, 74);
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = FlatStyle.Flat;
            this.btnCheckout.Dock = DockStyle.Top;
            this.btnCheckout.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            this.btnCheckout.ForeColor = Color.White;
            this.btnCheckout.Location = new Point(0, 10);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new Size(328, 82);
            this.btnCheckout.TabIndex = 0;
            this.btnCheckout.Text = "💳 THANH TOÁN (F9)";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Cursor = Cursors.Hand;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);

            // btnClearCart
            this.btnClearCart.BackColor = Color.FromArgb(220, 38, 38);
            this.btnClearCart.FlatAppearance.BorderSize = 0;
            this.btnClearCart.FlatStyle = FlatStyle.Flat;
            this.btnClearCart.Dock = DockStyle.Bottom;
            this.btnClearCart.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.btnClearCart.ForeColor = Color.White;
            this.btnClearCart.Location = new Point(0, 102);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new Size(328, 48);
            this.btnClearCart.TabIndex = 1;
            this.btnClearCart.Text = "🗑️ HỦY GIỎ HÀNG";
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Cursor = Cursors.Hand;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);

            // 
            // groupBoxPayment (Dock Fill in panelRight)
            // 
            this.groupBoxPayment.BackColor = Color.White;
            this.groupBoxPayment.Controls.Add(this.lblChange);
            this.groupBoxPayment.Controls.Add(this.label6);
            this.groupBoxPayment.Controls.Add(this.txtCashReceived);
            this.groupBoxPayment.Controls.Add(this.label5);
            this.groupBoxPayment.Controls.Add(this.lblTotalAmount);
            this.groupBoxPayment.Controls.Add(this.label3);
            this.groupBoxPayment.Dock = DockStyle.Fill;
            this.groupBoxPayment.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.groupBoxPayment.ForeColor = Color.FromArgb(30, 41, 59);
            this.groupBoxPayment.Location = new Point(12, 130);
            this.groupBoxPayment.Name = "groupBoxPayment";
            this.groupBoxPayment.Size = new Size(328, 356);
            this.groupBoxPayment.TabIndex = 1;
            this.groupBoxPayment.TabStop = false;
            this.groupBoxPayment.Text = "THÔNG TIN THANH TOÁN";

            // label3 (Tổng tiền)
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.label3.ForeColor = Color.FromArgb(71, 85, 105);
            this.label3.Location = new Point(18, 36);
            this.label3.Name = "label3";
            this.label3.Text = "Tổng tiền phải thu:";

            // lblTotalAmount
            this.lblTotalAmount.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTotalAmount.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblTotalAmount.Location = new Point(18, 62);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new Size(292, 46);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "0 đ";
            this.lblTotalAmount.TextAlign = ContentAlignment.MiddleRight;

            // label5 (Khách đưa)
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.label5.ForeColor = Color.FromArgb(71, 85, 105);
            this.label5.Location = new Point(18, 126);
            this.label5.Name = "label5";
            this.label5.Text = "Tiền khách đưa (VNĐ):";

            // txtCashReceived
            this.txtCashReceived.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.txtCashReceived.Location = new Point(18, 154);
            this.txtCashReceived.Name = "txtCashReceived";
            this.txtCashReceived.Size = new Size(292, 38);
            this.txtCashReceived.TabIndex = 0;
            this.txtCashReceived.TextAlign = HorizontalAlignment.Right;
            this.txtCashReceived.TextChanged += new System.EventHandler(this.txtCashReceived_TextChanged);

            // label6 (Tiền thừa)
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.label6.ForeColor = Color.FromArgb(71, 85, 105);
            this.label6.Location = new Point(18, 212);
            this.label6.Name = "label6";
            this.label6.Text = "Tiền thừa trả khách:";

            // lblChange
            this.lblChange.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblChange.ForeColor = Color.FromArgb(22, 163, 74);
            this.lblChange.Location = new Point(18, 240);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new Size(292, 40);
            this.lblChange.TabIndex = 5;
            this.lblChange.Text = "0 đ";
            this.lblChange.TextAlign = ContentAlignment.MiddleRight;

            // 
            // panelLeft (Left Barcode & Cart Grid - Dock Fill)
            // 
            this.panelLeft.BackColor = Color.White;
            this.panelLeft.Controls.Add(this.dgvCart);
            this.panelLeft.Controls.Add(this.panelBarcode);
            this.panelLeft.Dock = DockStyle.Fill;
            this.panelLeft.Location = new Point(16, 12);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new Padding(12);
            this.panelLeft.Size = new Size(678, 636);
            this.panelLeft.TabIndex = 0;

            // panelBarcode
            this.panelBarcode.BackColor = Color.White;
            this.panelBarcode.Controls.Add(this.lblBarcode);
            this.panelBarcode.Controls.Add(this.txtBarcode);
            this.panelBarcode.Dock = DockStyle.Top;
            this.panelBarcode.Location = new Point(12, 12);
            this.panelBarcode.Name = "panelBarcode";
            this.panelBarcode.Size = new Size(654, 48);
            this.panelBarcode.TabIndex = 0;

            // lblBarcode
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblBarcode.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblBarcode.Location = new Point(0, 10);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Text = "🔍 Quét mã vạch SP:";

            // txtBarcode (Compact width instead of overly wide)
            this.txtBarcode.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.txtBarcode.Location = new Point(165, 6);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.PlaceholderText = "Nhập mã vạch (vd: 893456789001 / SMH000016) + Enter...";
            this.txtBarcode.Size = new Size(340, 31);
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.KeyDown += new KeyEventHandler(this.txtBarcode_KeyDown);

            // dgvCart
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AllowUserToResizeRows = false;
            this.dgvCart.BackgroundColor = Color.White;
            this.dgvCart.BorderStyle = BorderStyle.None;
            this.dgvCart.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCart.GridColor = Color.FromArgb(226, 232, 240);
            this.dgvCart.EnableHeadersVisualStyles = false;
            this.dgvCart.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvCart.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Padding = new Padding(6)
            };
            this.dgvCart.ColumnHeadersHeight = 40;
            this.dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCart.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175),
                Padding = new Padding(6, 2, 6, 2)
            };
            this.dgvCart.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252)
            };
            this.dgvCart.Dock = DockStyle.Fill;
            this.dgvCart.Location = new Point(12, 60);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowTemplate.Height = 38;
            this.dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new Size(654, 564);
            this.dgvCart.TabIndex = 1;

            // 
            // FormPOS
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.ClientSize = new Size(1050, 660);
            this.Padding = new Padding(16, 12, 16, 12);

            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);

            this.FormBorderStyle = FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormPOS";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "POS - Bán hàng";
            this.KeyDown += new KeyEventHandler(this.FormPOS_KeyDown);

            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.panelBarcode.ResumeLayout(false);
            this.panelBarcode.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.groupBoxCustomer.ResumeLayout(false);
            this.groupBoxCustomer.PerformLayout();
            this.groupBoxPayment.ResumeLayout(false);
            this.groupBoxPayment.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panelLeft;
        private Panel panelRight;
        private DataGridView dgvCart;
        private Panel panelBarcode;
        private TextBox txtBarcode;
        private Label lblBarcode;
        private GroupBox groupBoxCustomer;
        private Label label1;
        private Label lblCustomerName;
        private Label label2;
        private TextBox txtCustomerPhone;
        private Panel panelActions;
        private Button btnCheckout;
        private Button btnClearCart;
        private GroupBox groupBoxPayment;
        private Label label3;
        private TextBox txtCashReceived;
        private Label label5;
        private Label lblTotalAmount;
        private Label lblChange;
        private Label label6;
    }
}
