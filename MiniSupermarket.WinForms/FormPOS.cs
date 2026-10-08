using MiniSupermarket.WinForms.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private readonly List<CartItemDto> _cart = new();

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();

            KeyPreview = true;
            KeyDown += FormPOS_KeyDown;

            txtBarcode.Focus();
        }

        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductId",
                HeaderText = "Mã SP",
                Width = 80
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên Sản Phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Đơn Giá",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0"
                }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "SL",
                Width = 70
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalPrice",
                HeaderText = "Thành Tiền",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0"
                }
            });
        }

        private async void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;

            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
                return;

            string barcode = txtBarcode.Text.Trim();
            txtBarcode.Clear();

            await AddProductToCartByBarcodeAsync(barcode);
        }

        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                // Đã sửa: Thêm SessionManager. vào trước ApiClientService
                var product = await SessionManager.ApiClientService.Client.GetFromJsonAsync<ProductDto>(
                    $"products/barcode/{barcode}");

                if (product == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy sản phẩm có mã vạch này!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtBarcode.Focus();
                    return;
                }

                var existingItem = _cart.FirstOrDefault(
                    c => c.ProductId == product.ProductId);

                if (existingItem != null)
                {
                    if (existingItem.Quantity >= product.StockQuantity)
                    {
                        MessageBox.Show(
                            "Số lượng sản phẩm trong kho không đủ!",
                            "Cảnh báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        txtBarcode.Focus();
                        return;
                    }

                    existingItem.Quantity++;
                }
                else
                {
                    if (product.StockQuantity <= 0)
                    {
                        MessageBox.Show(
                            "Sản phẩm đã hết hàng!",
                            "Cảnh báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        txtBarcode.Focus();
                        return;
                    }

                    _cart.Add(new CartItemDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }

                UpdateCartDisplay();
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối máy chủ: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart;

            decimal total = _cart.Sum(x => x.TotalPrice);

            lblTotalAmount.Text = $"{total:N0} đ";

            CalculateChange();
        }

        private void txtCashReceived_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateChange()
        {
            decimal total = _cart.Sum(x => x.TotalPrice);

            if (decimal.TryParse(
                txtCashReceived.Text.Trim(),
                out decimal cashReceived))
            {
                decimal change = cashReceived - total;

                if (change >= 0)
                {
                    lblChange.Text = $"{change:N0} đ";
                    lblChange.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblChange.Text = "Chưa đủ tiền!";
                    lblChange.ForeColor = Color.Red;
                }
            }
            else
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = Color.Black;
            }
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show(
                    "Giỏ hàng đang trống!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtBarcode.Focus();
                return;
            }

            if (!decimal.TryParse(
                txtCashReceived.Text.Trim(),
                out decimal cashReceived))
            {
                MessageBox.Show(
                    "Vui lòng nhập số tiền khách đưa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCashReceived.Focus();
                return;
            }

            decimal total = _cart.Sum(x => x.TotalPrice);

            if (cashReceived < total)
            {
                MessageBox.Show(
                    "Số tiền khách đưa chưa đủ!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCashReceived.Focus();
                return;
            }

            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                CashReceived = cashReceived,
                Items = _cart.Select(i => new
                {
                    i.ProductId,
                    i.Quantity,
                    i.UnitPrice
                }).ToList()
            };

            try
            {
                // Đã sửa: Thêm SessionManager. vào trước ApiClientService
                var response = await SessionManager.ApiClientService.Client.PostAsJsonAsync(
                    "orders/checkout",
                    orderRequest);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thanh toán thành công và đã in hóa đơn!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    _cart.Clear();

                    UpdateCartDisplay();

                    txtCashReceived.Clear();
                    txtCustomerPhone.Clear();

                    lblCustomerName.Text = "Khách vãng lai";

                    txtBarcode.Focus();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Thanh toán thất bại từ máy chủ!\n\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối máy chủ: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
                return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn hủy toàn bộ giỏ hàng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            _cart.Clear();

            UpdateCartDisplay();

            txtCashReceived.Clear();
            txtCustomerPhone.Clear();

            lblCustomerName.Text = "Khách vãng lai";

            txtBarcode.Focus();
        }

        private void FormPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                e.SuppressKeyPress = true;
                btnCheckout.PerformClick();
            }
        }
    }

    // Class CartItemDto dùng riêng cho giỏ hàng
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }

    // Đã xóa class ProductDto bị trùng lặp ở đây
}