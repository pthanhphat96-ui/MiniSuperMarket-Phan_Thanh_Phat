/*
 * Ho ten sinh vien: Phan Thanh Phat
 * Ngay tao: 22/10/2026
 * Mo ta: Controller ADO.NET thuan - Tra cuu ma vach sieu toc va demo DataTable (Buoi 5 - Tiet 1, 2, 4)
 */
using Microsoft.AspNetCore.Authorization;
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
    // DTO tra cuu san pham cho quay POS (kèm liên kết Category & Brand)
    public class ProductLookupDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
    }

    [Route("api/ado-products")]
    [ApiController]
    [Authorize] // Tiêu chí 29: Bảo vệ API bằng JWT Token
    public class AdoProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdoProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // THAO TÁC 1: TRA CỨU THEO MÃ VẠCH (CONNECTED MODE - SQLDATAREADER)
        // GET: api/ado-products/barcode/893456789001
        // =========================================================================
        [HttpGet("barcode/{barcode}")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> GetProductByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return BadRequest(new { message = "Mã vạch không được để trống!" });
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.ProductId, p.Barcode, p.ProductName, p.Price, p.StockQuantity,
                           p.CategoryId, COALESCE(c.CategoryName, 'Chưa phân loại') AS CategoryName,
                           p.BrandId, COALESCE(b.BrandName, 'Chưa rõ thương hiệu') AS BrandName
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    WHERE p.Barcode = @Barcode";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Barcode", barcode.Trim());

                    try
                    {
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var productDto = new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                    CategoryName = reader["CategoryName"].ToString() ?? string.Empty,
                                    BrandId = Convert.ToInt32(reader["BrandId"]),
                                    BrandName = reader["BrandName"].ToString() ?? string.Empty
                                };

                                return Ok(productDto);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
                    }
                }
            }

            return NotFound(new { message = $"Không tìm thấy sản phẩm có mã vạch: {barcode}" });
        }

        // =========================================================================
        // THAO TÁC 2: TẢI DANH SÁCH LƯU ĐỆM (DISCONNECTED MODE - DATATABLE & ADAPTER)
        // GET: api/ado-products/disconnected-cache
        // =========================================================================
        [HttpGet("disconnected-cache")]
        public async Task<IActionResult> GetProductsDisconnected()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            DataTable productTable = new DataTable("CachedProducts");

            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT p.ProductId, p.Barcode, p.ProductName, p.Price, p.StockQuantity,
                               p.CategoryId, COALESCE(c.CategoryName, 'Chưa phân loại') AS CategoryName,
                               p.BrandId, COALESCE(b.BrandName, 'Chưa rõ thương hiệu') AS BrandName
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                        LEFT JOIN Brands b ON p.BrandId = b.BrandId
                        ORDER BY p.ProductId ASC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(productTable);
                        }
                    }
                }
            });

            var resultList = new List<ProductLookupDto>();

            foreach (DataRow row in productTable.Rows)
            {
                resultList.Add(new ProductLookupDto
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Barcode = row["Barcode"].ToString() ?? string.Empty,
                    ProductName = row["ProductName"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(row["Price"]),
                    StockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    CategoryName = row["CategoryName"].ToString() ?? string.Empty,
                    BrandId = Convert.ToInt32(row["BrandId"]),
                    BrandName = row["BrandName"].ToString() ?? string.Empty
                });
            }

            return Ok(new
            {
                totalLoaded = productTable.Rows.Count,
                cachedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data = resultList
            });
        }

        // =========================================================================
        // BÀI TẬP 2.1 (TIẾT 2): TRA CỨU THEO KHOẢNG GIÁ TỒN KHO (SQLDATAREADER)
        // GET: api/ado-products/filter-stock?minStock=50
        // =========================================================================
        [HttpGet("filter-stock")]
        public async Task<IActionResult> FilterByStock([FromQuery] int minStock = 50)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            var resultList = new List<ProductLookupDto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.ProductId, p.Barcode, p.ProductName, p.Price, p.StockQuantity,
                           p.CategoryId, COALESCE(c.CategoryName, 'Chưa phân loại') AS CategoryName,
                           p.BrandId, COALESCE(b.BrandName, 'Chưa rõ thương hiệu') AS BrandName
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    WHERE p.StockQuantity <= @MinStock
                    ORDER BY p.StockQuantity ASC";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MinStock", minStock);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                resultList.Add(new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                    CategoryName = reader["CategoryName"].ToString() ?? string.Empty,
                                    BrandId = Convert.ToInt32(reader["BrandId"]),
                                    BrandName = reader["BrandName"].ToString() ?? string.Empty
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
        // GET: api/ado-products/count-by-category
        // =========================================================================
        [HttpGet("count-by-category")]
        public async Task<IActionResult> CountProductsByCategory()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            DataTable categoryCountTable = new DataTable("CategoryProductCount");

            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT p.CategoryId, COALESCE(c.CategoryName, 'Chưa phân loại') AS CategoryName, COUNT(*) AS Total
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                        GROUP BY p.CategoryId, c.CategoryName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(categoryCountTable);
                        }
                    }
                }
            });

            var summaryList = new List<object>();
            foreach (DataRow row in categoryCountTable.Rows)
            {
                summaryList.Add(new
                {
                    categoryId = Convert.ToInt32(row["CategoryId"]),
                    categoryName = row["CategoryName"].ToString() ?? string.Empty,
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
        // GET: api/ado-products/tiet1-count
        // =========================================================================
        [HttpGet("tiet1-count")]
        public async Task<IActionResult> CountTotalProductsScalar()
        {
            string connString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Products";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();
                        object? result = await command.ExecuteScalarAsync();
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
        // PUT: api/ado-products/tiet1-update-stock?productId=1&newStock=250
        // =========================================================================
        [HttpPut("tiet1-update-stock")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> UpdateProductStockNonQuery([FromQuery] int productId = 1, [FromQuery] int newStock = 250)
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

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();

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
        // POST: api/ado-products/tiet4-simulate-rollback
        // =========================================================================
        [HttpPost("tiet4-simulate-rollback")]
        public async Task<IActionResult> SimulateTransactionRollback([FromQuery] int productId = 1, [FromQuery] bool simulateError = true)
        {
            string connString = _configuration.GetConnectionString("DefaultConnection")!;
            var logs = new List<string>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                await connection.OpenAsync();
                SqlTransaction transaction = await connection.BeginTransactionAsync();

                try
                {
                    logs.Add("-> Bắt đầu phiên giao dịch...");

                    string sqlStep1 = "UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE ProductId = @Id";
                    using (SqlCommand cmd1 = new SqlCommand(sqlStep1, connection, transaction))
                    {
                        cmd1.Parameters.AddWithValue("@Qty", 5);
                        cmd1.Parameters.AddWithValue("@Id", productId);
                        int r1 = await cmd1.ExecuteNonQueryAsync();
                        logs.Add($"-> Bước 1: Đã trừ tồn kho SP {productId} (Số dòng tác động: {r1}).");
                    }

                    logs.Add("-> Bước 2: Chuẩn bị thực thi thao tác lỗi giả lập...");
                    if (simulateError)
                    {
                        throw new InvalidOperationException("Mặt hàng thứ hai đã hết tồn kho thực tế!");
                    }

                    await transaction.CommitAsync();
                    logs.Add("-> GIAO DỊCH COMMIT THÀNH CÔNG!");

                    return Ok(new { success = true, logs });
                }
                catch (Exception ex)
                {
                    logs.Add($"[PHÁT SINH LỖI]: {ex.Message}");
                    await transaction.RollbackAsync();
                    logs.Add("-> [HỆ THỐNG]: Đã kích hoạt transaction.Rollback()!");
                    logs.Add($"-> Toàn bộ thay đổi của Bước 1 đã được hoàn trả, tồn kho SP {productId} không bị trừ!");

                    return Ok(new { success = false, rolledBack = true, logs });
                }
            }
        }
    }
}
