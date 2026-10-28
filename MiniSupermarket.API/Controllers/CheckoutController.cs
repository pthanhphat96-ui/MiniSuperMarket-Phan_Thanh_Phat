/*
 * Ho ten sinh vien: Phan Thanh Phat
 * Ngay tao: 22/10/2026
 * Mo ta: API Thanh toan POS da bang duoc bao ve boi SqlTransaction (ACID) (Buoi 5 - Tiet 5)
 */
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Models;
using SqlConnection = MySqlConnector.MySqlConnection;
using SqlCommand = MySqlConnector.MySqlCommand;
using SqlDataReader = MySqlConnector.MySqlDataReader;
using SqlTransaction = MySqlConnector.MySqlTransaction;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")] // Tiêu chí 29 & 30: Admin và Thu ngân mới được thanh toán POS
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // POST: api/checkout
        // Chuc nang: Thanh toan hoa don, tru ton kho va tich diem trong 1 Transaction
        // =========================================================================
        [HttpPost]
        public async Task<IActionResult> ProcessCheckout([FromBody] CheckoutRequestDto request)
        {
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new { message = "Giỏ hàng rỗng, không thể tiến hành thanh toán!" });
            }

            if (string.IsNullOrWhiteSpace(request.CashierUsername))
            {
                return BadRequest(new { message = "Thiếu thông tin tài khoản thu ngân lập hóa đơn!" });
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // KHOI TAO TRANSACTION (Bat dau phien giao dich khep kin bao ve ACID - Tiêu chí 35)
                SqlTransaction transaction = await connection.BeginTransactionAsync();

                try
                {
                    decimal finalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice);
                    int totalItems = request.Items.Sum(i => i.Quantity);
                    int rewardPoints = (int)(finalAmount / 10000);
                    string orderCode = $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

                    // Tra cứu khóa ngoại CashierId từ bảng Users
                    int cashierId = 8;
                    string findCashierSql = "SELECT UserId FROM Users WHERE Username = @Username LIMIT 1";
                    using (SqlCommand cmdCashier = new SqlCommand(findCashierSql, connection, transaction))
                    {
                        cmdCashier.Parameters.AddWithValue("@Username", request.CashierUsername.Trim());
                        object? foundId = await cmdCashier.ExecuteScalarAsync();
                        if (foundId != null && foundId != DBNull.Value)
                        {
                            cashierId = Convert.ToInt32(foundId);
                        }
                    }

                    // =====================================================================
                    // BƯỚC 1: TAO HOA DON TONG THE TRONG BANG Orders
                    // =====================================================================
                    string insertOrderSql = @"
                        INSERT INTO Orders (
                            OrderCode, CreatedDate, CashierUsername, CustomerId, TotalItems, FinalAmount, RewardPoints,
                            Subtotal, Discount, Total, PaymentMethod, Status, CreatedAt, CashierId
                        )
                        VALUES (
                            @OrderCode, NOW(), @CashierUsername, @CustomerId, @TotalItems, @FinalAmount, @RewardPoints,
                            @FinalAmount, 0, @FinalAmount, 'CASH', 'PAID', NOW(), @CashierId
                        );
                        SELECT LAST_INSERT_ID();";

                    int orderId = 0;
                    using (SqlCommand cmdOrder = new SqlCommand(insertOrderSql, connection, transaction))
                    {
                        cmdOrder.Parameters.AddWithValue("@OrderCode", orderCode);
                        cmdOrder.Parameters.AddWithValue("@CashierUsername", request.CashierUsername.Trim());
                        cmdOrder.Parameters.AddWithValue("@CustomerId", request.CustomerId > 0 ? request.CustomerId : (object)DBNull.Value);
                        cmdOrder.Parameters.AddWithValue("@TotalItems", totalItems);
                        cmdOrder.Parameters.AddWithValue("@FinalAmount", finalAmount);
                        cmdOrder.Parameters.AddWithValue("@RewardPoints", rewardPoints);
                        cmdOrder.Parameters.AddWithValue("@CashierId", cashierId);

                        orderId = Convert.ToInt32(await cmdOrder.ExecuteScalarAsync());
                    }

                    // =====================================================================
                    // BƯỚC 2 & 3: XU LY TUNG MON HANG (KIEM TRA, LUU CHI TIET VA TRU TON KHO)
                    // =====================================================================
                    foreach (var item in request.Items)
                    {
                        string checkStockSql = "SELECT StockQuantity, ProductName FROM Products WHERE ProductId = @ProductId";
                        int currentStock = 0;
                        string productName = string.Empty;

                        using (SqlCommand cmdCheck = new SqlCommand(checkStockSql, connection, transaction))
                        {
                            cmdCheck.Parameters.AddWithValue("@ProductId", item.ProductId);

                            using (SqlDataReader reader = await cmdCheck.ExecuteReaderAsync())
                            {
                                if (!await reader.ReadAsync())
                                {
                                    throw new InvalidOperationException($"Không tìm thấy sản phẩm có ID: {item.ProductId}");
                                }
                                currentStock = Convert.ToInt32(reader["StockQuantity"]);
                                productName = reader["ProductName"].ToString() ?? string.Empty;
                            }
                        }

                        if (currentStock < item.Quantity)
                        {
                            throw new InvalidOperationException(
                                $"Sản phẩm '{productName}' (ID: {item.ProductId}) không đủ tồn kho! " +
                                $"(Tồn kho thực tế: {currentStock}, Số lượng yêu cầu mua: {item.Quantity})");
                        }

                        string insertDetailSql = @"
                            INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
                            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal)";

                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailSql, connection, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@OrderId", orderId);
                            cmdDetail.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdDetail.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            cmdDetail.Parameters.AddWithValue("@LineTotal", item.Quantity * item.UnitPrice);
                            await cmdDetail.ExecuteNonQueryAsync();
                        }

                        string updateStockSql = @"
                            UPDATE Products
                            SET StockQuantity = StockQuantity - @Qty
                            WHERE ProductId = @ProductId";

                        using (SqlCommand cmdStock = new SqlCommand(updateStockSql, connection, transaction))
                        {
                            cmdStock.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdStock.Parameters.AddWithValue("@ProductId", item.ProductId);
                            await cmdStock.ExecuteNonQueryAsync();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 4: CONG DIEM TICH LUY CHO KHACH HANG TRONG BANG Customers
                    // =====================================================================
                    if (rewardPoints > 0 && request.CustomerId > 0)
                    {
                        string updatePointsSql = @"
                            UPDATE Customers
                            SET RewardPoints = RewardPoints + @Points
                            WHERE CustomerId = @CustomerId";

                        using (SqlCommand cmdCustomer = new SqlCommand(updatePointsSql, connection, transaction))
                        {
                            cmdCustomer.Parameters.AddWithValue("@Points", rewardPoints);
                            cmdCustomer.Parameters.AddWithValue("@CustomerId", request.CustomerId);
                            await cmdCustomer.ExecuteNonQueryAsync();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 5: XAC NHAN LUU VINH VIEN TOAN BO THAO TAC (COMMIT)
                    // =====================================================================
                    await transaction.CommitAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Thanh toán hóa đơn POS thành công!",
                        orderId = orderId,
                        orderCode = orderCode,
                        totalItems = totalItems,
                        finalAmount = finalAmount,
                        rewardPointsAdded = rewardPoints
                    });
                }
                catch (Exception ex)
                {
                    // =====================================================================
                    // BƯỚC 6: HOAN TAC TOAN BO DU LIEU SACH SE KHI CO BAT KY LOI NAO (ROLLBACK)
                    // =====================================================================
                    try
                    {
                        await transaction.RollbackAsync();
                    }
                    catch (Exception rollbackEx)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi nghiêm trọng khi thực thi Rollback: " + rollbackEx.Message
                        });
                    }

                    return BadRequest(new
                    {
                        success = false,
                        message = "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
                        errorDetail = ex.Message
                    });
                }
            }
        }

        // =========================================================================
        // BÀI TẬP MỞ RỘNG (MỤC 5 - TIẾT 5): API HỦY ĐƠN HÀNG VÀ HOÀN TRẢ TỒN KHO
        // DELETE: api/checkout/cancel/{orderId}
        // =========================================================================
        [HttpDelete("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlTransaction transaction = await connection.BeginTransactionAsync();

                try
                {
                    string getOrderSql = "SELECT CustomerId, RewardPoints FROM Orders WHERE OrderId = @OrderId";
                    int customerId = 0;
                    int pointsToDeduct = 0;

                    using (SqlCommand cmdGet = new SqlCommand(getOrderSql, connection, transaction))
                    {
                        cmdGet.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader reader = await cmdGet.ExecuteReaderAsync())
                        {
                            if (!await reader.ReadAsync())
                            {
                                throw new InvalidOperationException($"Không tìm thấy hóa đơn ID = {orderId} để hủy!");
                            }
                            customerId = reader["CustomerId"] != DBNull.Value
                                ? Convert.ToInt32(reader["CustomerId"])
                                : 0;
                            pointsToDeduct = reader["RewardPoints"] != DBNull.Value
                                ? Convert.ToInt32(reader["RewardPoints"])
                                : 0;
                        }
                    }

                    var itemsToRestore = new List<(int ProductId, int Quantity)>();
                    string getDetailsSql = "SELECT ProductId, Quantity FROM OrderDetails WHERE OrderId = @OrderId";

                    using (SqlCommand cmdDetails = new SqlCommand(getDetailsSql, connection, transaction))
                    {
                        cmdDetails.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader reader = await cmdDetails.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                itemsToRestore.Add((
                                    Convert.ToInt32(reader["ProductId"]),
                                    Convert.ToInt32(reader["Quantity"])
                                ));
                            }
                        }
                    }

                    foreach (var item in itemsToRestore)
                    {
                        string restoreStockSql = @"
                            UPDATE Products
                            SET StockQuantity = StockQuantity + @Qty
                            WHERE ProductId = @ProductId";

                        using (SqlCommand cmdRestore = new SqlCommand(restoreStockSql, connection, transaction))
                        {
                            cmdRestore.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdRestore.Parameters.AddWithValue("@ProductId", item.ProductId);
                            await cmdRestore.ExecuteNonQueryAsync();
                        }
                    }

                    if (pointsToDeduct > 0 && customerId > 0)
                    {
                        string deductPointsSql = @"
                            UPDATE Customers
                            SET RewardPoints = CASE
                                WHEN RewardPoints >= @Pts THEN RewardPoints - @Pts
                                ELSE 0
                            END
                            WHERE CustomerId = @CustomerId";

                        using (SqlCommand cmdDeduct = new SqlCommand(deductPointsSql, connection, transaction))
                        {
                            cmdDeduct.Parameters.AddWithValue("@Pts", pointsToDeduct);
                            cmdDeduct.Parameters.AddWithValue("@CustomerId", customerId);
                            await cmdDeduct.ExecuteNonQueryAsync();
                        }
                    }

                    string deleteDetailsSql = "DELETE FROM OrderDetails WHERE OrderId = @OrderId";
                    using (SqlCommand cmdDelDetails = new SqlCommand(deleteDetailsSql, connection, transaction))
                    {
                        cmdDelDetails.Parameters.AddWithValue("@OrderId", orderId);
                        await cmdDelDetails.ExecuteNonQueryAsync();
                    }

                    string deleteOrderSql = "DELETE FROM Orders WHERE OrderId = @OrderId";
                    using (SqlCommand cmdDelOrder = new SqlCommand(deleteOrderSql, connection, transaction))
                    {
                        cmdDelOrder.Parameters.AddWithValue("@OrderId", orderId);
                        await cmdDelOrder.ExecuteNonQueryAsync();
                    }

                    await transaction.CommitAsync();

                    return Ok(new
                    {
                        success = true,
                        message = $"Đã hủy thành công hóa đơn #{orderId} và hoàn trả toàn bộ tồn kho vào CSDL!"
                    });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new
                    {
                        success = false,
                        message = "Hủy hóa đơn thất bại, giao dịch đã được Rollback!",
                        errorDetail = ex.Message
                    });
                }
            }
        }
    }
}
