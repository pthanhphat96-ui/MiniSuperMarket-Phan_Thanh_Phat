/*
 * Ho ten sinh vien: Phan Thanh Phat
 * Ngay tao: 22/10/2026
 * Mo ta: Controller ADO.NET thuan - Tra cuu ma vach sieu toc va demo DataTable (Buoi 5 - Tiet 1, 2, 4)
 */
using Microsoft.AspNetCore.Mvc;
using System.Data;
// Sử dụng MySqlConnector ánh xạ tương thích chuẩn ADO.NET (SqlConnection, SqlCommand, SqlDataReader, SqlDataAdapter, SqlTransaction)
// Nếu đổi sang SQL Server chỉ cần mở comment dòng dưới và đóng khối alias MySqlConnector:
// using Microsoft.Data.SqlClient;
using SqlConnection = MySqlConnector.MySqlConnection;
using SqlCommand = MySqlConnector.MySqlCommand;
using SqlDataReader = MySqlConnector.MySqlDataReader;
using SqlDataAdapter = MySqlConnector.MySqlDataAdapter;
using SqlTransaction = MySqlConnector.MySqlTransaction;
using SqlException = MySqlConnector.MySqlException;

namespace MiniSupermarket.API.Controllers
{
    // DTO tra cuu san pham cho quay POS
    public class ProductLookupDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    [Route("api/ado-products")]
    [ApiController]
    public class AdoProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Constructor tiêm IConfiguration de doc chuoi ket noi tu appsettings.json
        public AdoProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // THAO TÁC 1: TRA CỨU THEO MÃ VẠCH (CONNECTED MODE - SQLDATAREADER)
        // =========================================================================
        // GET: api/ado-products/barcode/893456789001
        [HttpGet("barcode/{barcode}")]
        public IActionResult GetProductByBarcode(string barcode)
        {
            // 1. Kiem tra tinh hop le cua tham so dau vao
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return BadRequest(new { message = "Mã vạch không được để trống!" });
            }

            // 2. Lay chuoi ket noi toi CSDL
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // 3. Su dung khoi using de dam bao SqlConnection luon duoc dong va giai phong
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 4. Cau lenh SELECT co tham so hoa @Barcode chong SQL Injection
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                 FROM Products
                                 WHERE Barcode = @Barcode";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 5. Gan gia tri an toan qua SqlParameter
                    command.Parameters.AddWithValue("@Barcode", barcode.Trim());

                    try
                    {
                        // 6. Mo ket noi vat ly toi CSDL
                        connection.Open();

                        // 7. Khoi tao luong doc nhanh Forward-Only SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 8. Doc ban ghi: reader.Read() tra ve true neu tim thay dong du lieu
                            if (reader.Read())
                            {
                                var productDto = new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                };

                                // Tra ve ma 200 OK kem du lieu san pham
                                return Ok(productDto);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Bat loi neu ket noi CSDL gap su co
                        return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
                    }
                }
            } // Ket thuc using, connection.Close() tu dong duoc thuc thi tai day

            // 9. Neu khong tim thay san pham nao khop ma vach -> Tra ve 404 Not Found
            return NotFound(new { message = $"Không tìm thấy sản phẩm có mã vạch: {barcode}" });
        }

        // =========================================================================
        // THAO TÁC 2: TẢI DANH SÁCH LƯU ĐỆM (DISCONNECTED MODE - DATATABLE & ADAPTER)
        // =========================================================================
        // GET: api/ado-products/disconnected-cache
        [HttpGet("disconnected-cache")]
        public IActionResult GetProductsDisconnected()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // 1. Khoi tao bang ao DataTable tren RAM
            DataTable productTable = new DataTable("CachedProducts");

            // 2. Mo ket noi va su dung SqlDataAdapter de nap du lieu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                 FROM Products
                                 ORDER BY ProductId ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 3. SqlDataAdapter lam cau noi giua Command va DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // 4. Phuong thuc Fill(): Tu dong Open connection -> Doc du lieu do vao DataTable -> Tu dong Close connection
                        adapter.Fill(productTable);
                    }
                }
            } // Tai day, ket noi CSDL DA DUOC DONG HOAN TOAN!

            // 5. Thao tac doc lap tren bo nho RAM (Khong can ket noi toi CSDL nua)
            var resultList = new List<ProductLookupDto>();

            // Duyet qua tung dong (DataRow) trong tap hop Rows cua DataTable
            foreach (DataRow row in productTable.Rows)
            {
                resultList.Add(new ProductLookupDto
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Barcode = row["Barcode"].ToString() ?? string.Empty,
                    ProductName = row["ProductName"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(row["Price"]),
                    StockQuantity = Convert.ToInt32(row["StockQuantity"])
                });
            }

            // Tra ve danh sach san pham da duoc luu dem tren RAM
            return Ok(new
            {
                totalLoaded = productTable.Rows.Count,
                cachedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data = resultList
            });
        }

        // =========================================================================
        // BÀI TẬP 2.1 (TIẾT 2): TRA CỨU THEO KHOẢNG GIÁ TỒN KHO (SQLDATAREADER)
        // =========================================================================
        // GET: api/ado-products/filter-stock?minStock=50
        [HttpGet("filter-stock")]
        public IActionResult FilterByStock([FromQuery] int minStock = 50)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            var resultList = new List<ProductLookupDto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                 FROM Products
                                 WHERE StockQuantity <= @MinStock
                                 ORDER BY StockQuantity ASC";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MinStock", minStock);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resultList.Add(new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                });
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
                    }
                }
            }

            return Ok(new
            {
                minStockThreshold = minStock,
                totalWarningProducts = resultList.Count,
                data = resultList
            });
        }

        // =========================================================================
        // BÀI TẬP 2.2 (TIẾT 2): ĐẾM SỐ LƯỢNG SẢN PHẨM THEO NHÓM DANH MỤC BẰNG DATATABLE
        // =========================================================================
        // GET: api/ado-products/count-by-category
        [HttpGet("count-by-category")]
        public IActionResult CountProductsByCategory()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            DataTable categoryCountTable = new DataTable("CategoryProductCount");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT CategoryId, COUNT(*) AS Total
                                 FROM Products
                                 GROUP BY CategoryId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(categoryCountTable);
                    }
                }
            }

            var summaryList = new List<object>();
            foreach (DataRow row in categoryCountTable.Rows)
            {
                summaryList.Add(new
                {
                    categoryId = Convert.ToInt32(row["CategoryId"]),
                    total = Convert.ToInt32(row["Total"])
                });
            }

            return Ok(new
            {
                totalCategories = summaryList.Count,
                data = summaryList
            });
        }

        // =========================================================================
        // BÀI TẬP 1 (TIẾT 1): ĐẾM TỔNG SỐ MẶT HÀNG BẰNG EXECUTESCALAR()
        // =========================================================================
        // GET: api/ado-products/tiet1-count
        [HttpGet("tiet1-count")]
        public IActionResult CountTotalProductsScalar()
        {
            string connString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Products";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object? result = command.ExecuteScalar();
                        int totalCount = Convert.ToInt32(result);

                        return Ok(new
                        {
                            message = $"Tổng số mặt hàng hiện có trong kho siêu thị: {totalCount} sản phẩm.",
                            totalCount = totalCount
                        });
                    }
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, new { message = $"[LỖI SQL]: {ex.Message}" });
                }
            }
        }

        // =========================================================================
        // BÀI TẬP 2 (TIẾT 1): CẬP NHẬT TỒN KHO BẰNG EXECUTENONQUERY() & SQLPARAMETER
        // =========================================================================
        // PUT: api/ado-products/tiet1-update-stock?productId=1&newStock=250
        [HttpPut("tiet1-update-stock")]
        public IActionResult UpdateProductStockNonQuery([FromQuery] int productId = 1, [FromQuery] int newStock = 250)
        {
            string connString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    string updateSql = @"UPDATE Products
                                         SET StockQuantity = @Stock
                                         WHERE ProductId = @Id";

                    using (SqlCommand command = new SqlCommand(updateSql, connection))
                    {
                        command.Parameters.AddWithValue("@Stock", newStock);
                        command.Parameters.AddWithValue("@Id", productId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new
                            {
                                success = true,
                                message = $"THÀNH CÔNG: Đã cập nhật tồn kho cho Sản phẩm ID = {productId} thành {newStock} đơn vị.",
                                rowsAffected = rowsAffected
                            });
                        }
                        else
                        {
                            return NotFound(new
                            {
                                success = false,
                                message = $"CẢNH BÁO: Không tìm thấy sản phẩm có ID = {productId} để cập nhật."
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, new { message = $"[LỖI SQL]: {ex.Message}" });
                }
            }
        }

        // =========================================================================
        // BÀI TẬP MÔ PHỎNG (TIẾT 4): KIỂM CHỨNG CƠ CHẾ ROLLBACK VỚI SQLTRANSACTION
        // =========================================================================
        // POST: api/ado-products/tiet4-simulate-rollback
        [HttpPost("tiet4-simulate-rollback")]
        public IActionResult SimulateTransactionRollback([FromQuery] int productId = 1, [FromQuery] bool simulateError = true)
        {
            string connString = _configuration.GetConnectionString("DefaultConnection")!;
            var logs = new List<string>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    logs.Add("-> Bắt đầu phiên giao dịch...");

                    string sqlStep1 = "UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE ProductId = @Id";
                    using (SqlCommand cmd1 = new SqlCommand(sqlStep1, connection, transaction))
                    {
                        cmd1.Parameters.AddWithValue("@Qty", 5);
                        cmd1.Parameters.AddWithValue("@Id", productId);
                        int r1 = cmd1.ExecuteNonQuery();
                        logs.Add($"-> Bước 1: Đã trừ tồn kho SP {productId} (Số dòng tác động: {r1}).");
                    }

                    logs.Add("-> Bước 2: Chuẩn bị thực thi thao tác lỗi giả lập...");
                    if (simulateError)
                    {
                        throw new InvalidOperationException("Mặt hàng thứ hai đã hết tồn kho thực tế!");
                    }

                    transaction.Commit();
                    logs.Add("-> GIAO DỊCH COMMIT THÀNH CÔNG!");

                    return Ok(new { success = true, logs });
                }
                catch (Exception ex)
                {
                    logs.Add($"[PHÁT SINH LỖI]: {ex.Message}");
                    transaction.Rollback();
                    logs.Add("-> [HỆ THỐNG]: Đã kích hoạt transaction.Rollback()!");
                    logs.Add($"-> Toàn bộ thay đổi của Bước 1 đã được hoàn trả, tồn kho SP {productId} không bị trừ!");

                    return Ok(new { success = false, rolledBack = true, logs });
                }
            }
        }
    }
}
