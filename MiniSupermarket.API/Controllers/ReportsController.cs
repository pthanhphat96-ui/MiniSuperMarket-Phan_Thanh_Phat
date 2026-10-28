/*
 * Ho ten sinh vien: Phan Thanh Phat
 * Ngay tao: 22/10/2026
 * Mo ta: Controller ADO.NET thuc thi Stored Procedure xuat bao cao ca ban (Buoi 5 - Tiet 3)
 */
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Data;
using SqlConnection = MySqlConnector.MySqlConnection;
using SqlCommand = MySqlConnector.MySqlCommand;
using SqlDataReader = MySqlConnector.MySqlDataReader;
using SqlException = MySqlConnector.MySqlException;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Tiêu chí 29 & 30: Chỉ Admin được xem báo cáo doanh thu
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _db;

        public ReportsController(IConfiguration configuration, SupermarketDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }

        // =========================================================================
        // GET: api/reports/revenue-by-cashier
        // Chuc nang: Goi Stored Procedure sp_GetRevenueByCashier qua SqlDataReader
        // =========================================================================
        [HttpGet("revenue-by-cashier")]
        public async Task<IActionResult> GetRevenueByCashier()
        {
            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByCashier", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new CashierRevenueDto
                                {
                                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    TotalRevenue = reader["TotalRevenue"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["TotalRevenue"])
                                        : 0m,
                                    TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value
                                        ? Convert.ToInt32(reader["TotalPointsAwarded"])
                                        : 0
                                };

                                reportList.Add(item);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi thực thi Stored Procedure trên CSDL: " + ex.Message
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        // =========================================================================
        // BÀI TẬP 3.1 (TIẾT 3): STORED PROCEDURE BÁO CÁO DOANH THU THEO KHOẢNG NGÀY
        // GET: api/reports/revenue-by-date-range?fromDate=2026-01-01&toDate=2026-12-31
        // =========================================================================
        [HttpGet("revenue-by-date-range")]
        public async Task<IActionResult> GetRevenueByDateRange(
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            DateTime start = fromDate ?? new DateTime(2026, 1, 1);
            DateTime end = toDate ?? new DateTime(2026, 12, 31, 23, 59, 59);

            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByDateRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("FromDate", start);
                    command.Parameters.AddWithValue("ToDate", end);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                reportList.Add(new CashierRevenueDto
                                {
                                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    TotalRevenue = reader["TotalRevenue"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["TotalRevenue"])
                                        : 0m,
                                    TotalPointsAwarded = reader.FieldCount > 3 && reader["TotalPointsAwarded"] != DBNull.Value
                                        ? Convert.ToInt32(reader["TotalPointsAwarded"])
                                        : 0
                                });
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi thực thi Stored Procedure sp_GetRevenueByDateRange: " + ex.Message
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                fromDate = start.ToString("yyyy-MM-dd HH:mm:ss"),
                toDate = end.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        // =========================================================================
        // BÀI TẬP 3.2 (TIẾT 3): STORED PROCEDURE TOP 5 SẢN PHẨM BÁN CHẠY NHẤT
        // GET: api/reports/top5-selling-products
        // =========================================================================
        [HttpGet("top5-selling-products")]
        public async Task<IActionResult> GetTop5SellingProducts()
        {
            var topProducts = new List<TopSellingProductDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetTop5SellingProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                topProducts.Add(new TopSellingProductDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    TotalQuantitySold = reader["TotalQuantitySold"] != DBNull.Value
                                        ? Convert.ToInt32(reader["TotalQuantitySold"])
                                        : 0,
                                    TotalRevenue = reader["TotalRevenue"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["TotalRevenue"])
                                        : 0m
                                });
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi thực thi Stored Procedure sp_GetTop5SellingProducts: " + ex.Message
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data = topProducts
            });
        }

        // =========================================================================
        // GIỮ LẠI ENDPOINT BÁO CÁO NGÀY PHỤC VỤ WINFORMS (FormReportManagement)
        // GET: api/reports/daily?date=2026-10-22
        // =========================================================================
        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyReport([FromQuery] string? date)
        {
            DateTime targetDate;

            if (!DateTime.TryParse(date, out targetDate))
            {
                targetDate = DateTime.Today;
            }

            var startDate = targetDate.Date;
            var endDate = startDate.AddDays(1);

            var orders = await _db.Orders
                .Where(o =>
                    o.CreatedAt >= startDate &&
                    o.CreatedAt < endDate &&
                    o.Status == "PAID")
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Include(o => o.Customer)
                .Include(o => o.Cashier)
                .AsNoTracking()
                .ToListAsync();

            int totalOrders = orders.Count;
            decimal totalRevenue = orders.Sum(o => o.Total);

            var bestSellerGroup = orders
                .SelectMany(o => o.Items)
                .GroupBy(i => new
                {
                    i.ProductId,
                    ProductName = i.Product != null
                        ? i.Product.ProductName
                        : $"SP #{i.ProductId}"
                })
                .Select(g => new
                {
                    g.Key.ProductName,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalAmount = g.Sum(x => x.LineTotal)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .FirstOrDefault();

            string bestSeller = bestSellerGroup != null
                ? $"{bestSellerGroup.ProductName} ({bestSellerGroup.TotalQuantity} sp)"
                : "Chưa có";

            var recentOrders = orders
                .OrderByDescending(o => o.CreatedAt)
                .Take(20)
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderCode,
                    o.CreatedAt,
                    o.Total,
                    o.Status,
                    CustomerName = o.Customer != null
                        ? o.Customer.CustomerName
                        : "Khách vãng lai",
                    CashierName = o.Cashier != null
                        ? o.Cashier.FullName
                        : o.CashierUsername
                })
                .ToList();

            return Ok(new
            {
                Date = startDate.ToString("yyyy-MM-dd"),
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                BestSeller = bestSeller,
                RecentOrders = recentOrders
            });
        }
    }
}
