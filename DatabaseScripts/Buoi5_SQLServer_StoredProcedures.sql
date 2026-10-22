-- =========================================================================
-- Ho ten sinh vien: Phan Thanh Phat
-- Mo ta: Script T-SQL (SQL Server SSMS) cho Thực hành Buổi 5
-- =========================================================================

-- 1. TẠO STORED PROCEDURE TIẾT 3: sp_GetRevenueByCashier
IF OBJECT_ID('sp_GetRevenueByCashier', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByCashier;
GO

CREATE PROCEDURE sp_GetRevenueByCashier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END
GO

-- 2. BÀI TẬP 3.1: sp_GetRevenueByDateRange
IF OBJECT_ID('sp_GetRevenueByDateRange', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByDateRange;
GO

CREATE PROCEDURE sp_GetRevenueByDateRange
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    WHERE CreatedDate >= @FromDate AND CreatedDate <= @ToDate
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END
GO

-- 3. BÀI TẬP 3.2: sp_GetTop5SellingProducts
IF OBJECT_ID('sp_GetTop5SellingProducts', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetTop5SellingProducts;
GO

CREATE PROCEDURE sp_GetTop5SellingProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 5
        p.ProductId,
        p.Barcode,
        p.ProductName,
        SUM(od.Quantity) AS TotalQuantitySold,
        SUM(od.LineTotal) AS TotalRevenue
    FROM OrderDetails od
    INNER JOIN Products p ON od.ProductId = p.ProductId
    GROUP BY p.ProductId, p.Barcode, p.ProductName
    ORDER BY TotalQuantitySold DESC, TotalRevenue DESC;
END
GO
