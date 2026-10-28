using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms
{
    public partial class FormOrderManagement : Form
    {
        private int _selectedOrderId = 0;

        public FormOrderManagement()
        {
            InitializeComponent();
        }

        private async void FormOrderManagement_Load(object sender, EventArgs e)
        {
            await LoadOrders();
        }

        private async System.Threading.Tasks.Task LoadOrders()
        {
            try
            {
                var orders = await ApiClientService.Client.GetFromJsonAsync<List<OrderDto>>("orders");
                dgvOrders.DataSource = orders;

                if (dgvOrders.Columns["OrderDetails"] != null)
                {
                    dgvOrders.Columns["OrderDetails"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _selectedOrderId = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["OrderId"].Value);
                var selectedOrder = (OrderDto)dgvOrders.Rows[e.RowIndex].DataBoundItem;
                dgvOrderDetails.DataSource = selectedOrder.OrderDetails;
            }
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            await UpdateOrderStatus("Đã duyệt");
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            await UpdateOrderStatus("Đã hủy");
        }

        private async System.Threading.Tasks.Task UpdateOrderStatus(string status)
        {
            if (_selectedOrderId == 0)
            {
                MessageBox.Show("Vui lòng click chọn một đơn hàng trên lưới!");
                return;
            }

            var response = await ApiClientService.Client.PutAsJsonAsync($"orders/{_selectedOrderId}/status", status);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Đã chuyển trạng thái đơn hàng thành: {status}");
                await LoadOrders();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CashierUsername { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderDetailDto> OrderDetails { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
