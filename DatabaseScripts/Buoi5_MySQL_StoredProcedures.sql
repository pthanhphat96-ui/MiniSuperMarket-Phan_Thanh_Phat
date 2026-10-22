-- =========================================================================
-- Ho ten sinh vien: Phan Thanh Phat
-- Mo ta: Script MySQL (XAMPP) cho Thực hành Buổi 5 (mini_phat)
-- Bao gồm:
--   1. Bổ sung các cột tương thích Buổi 5 cho bảng Orders (CreatedDate, CashierUsername, TotalItems, FinalAmount, RewardPoints)
--   2. Tạo View OrderDetails ánh xạ trực tiếp với bảng OrderItems
--   3. Bổ sung 5 sản phẩm mẫu (ID 1..5, Barcode 893456789001..893456789005) để kiểm thử Swagger theo đúng tài liệu
--   4. Tạo 3 Stored Procedure: sp_GetRevenueByCashier, sp_GetRevenueByDateRange, sp_GetTop5SellingProducts
-- =========================================================================

USE mini_phat;

-- 1. Bổ sung cột vào bảng Orders (nếu chưa có)
ALTER TABLE Orders
    ADD COLUMN IF NOT EXISTS CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ADD COLUMN IF NOT EXISTS CashierUsername VARCHAR(50) NOT NULL DEFAULT 'cashier01',
    ADD COLUMN IF NOT EXISTS TotalItems INT NOT NULL DEFAULT 1,
    ADD COLUMN IF NOT EXISTS FinalAmount DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    ADD COLUMN IF NOT EXISTS RewardPoints INT NOT NULL DEFAULT 0;

-- Đồng bộ dữ liệu các cột mới từ dữ liệu Orders/Users/OrderItems hiện có
UPDATE Orders o
LEFT JOIN Users u ON o.CashierId = u.UserId
SET
    o.CreatedDate = o.CreatedAt,
    o.CashierUsername = COALESCE(u.Username, CONCAT('cashier0', (o.OrderId % 5) + 1)),
    o.FinalAmount = o.Total,
    o.RewardPoints = FLOOR(o.Total / 10000),
    o.TotalItems = COALESCE((SELECT SUM(oi.Quantity) FROM OrderItems oi WHERE oi.OrderId = o.OrderId), 1)
WHERE o.FinalAmount = 0 OR o.CashierUsername = '' OR o.CashierUsername = 'cashier01';

-- 2. Tạo View OrderDetails (Updatable View trong MySQL/MariaDB) trỏ trực tiếp vào OrderItems
DROP VIEW IF EXISTS OrderDetails;
CREATE VIEW OrderDetails AS
SELECT
    OrderItemId AS OrderDetailId,
    OrderId,
    ProductId,
    Quantity,
    UnitPrice,
    LineTotal
FROM OrderItems;

-- 3. Bổ sung 5 sản phẩm mẫu chuẩn tài liệu Buổi 5 (ID 1 -> 5) nếu chưa tồn tại
INSERT IGNORE INTO Products (ProductId, Barcode, ProductName, Price, StockQuantity, CategoryId, BrandId)
VALUES
    (1, '893456789001', 'Snack Khoai Tây O''Star Gói 60g', 12500.00, 150, 1, 1),
    (2, '893456789002', 'Bánh Quy Bơ Danisa Hộp Thiếc 454g', 135000.00, 40, 1, 1),
    (3, '893456789003', 'Kẹo Dẻo Chupa Chups Hương Trái Cây', 15000.00, 120, 1, 1),
    (4, '893456789004', 'Bánh Chocopie Orion Hộp 12 Cái', 54000.00, 60, 1, 1),
    (5, '893456789005', 'Nước Ngọt Coca Cola Lon 320ml', 10000.00, 320, 2, 1);

-- =========================================================================
-- 4. STORED PROCEDURE TIẾT 3: sp_GetRevenueByCashier
-- =========================================================================
DROP PROCEDURE IF EXISTS sp_GetRevenueByCashier;

DELIMITER $$
CREATE PROCEDURE sp_GetRevenueByCashier()
BEGIN
    SELECT
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END$$
DELIMITER ;

-- =========================================================================
-- 5. BÀI TẬP 3.1: sp_GetRevenueByDateRange
-- =========================================================================
DROP PROCEDURE IF EXISTS sp_GetRevenueByDateRange;

DELIMITER $$
CREATE PROCEDURE sp_GetRevenueByDateRange(
    IN FromDate DATETIME,
    IN ToDate DATETIME
)
BEGIN
    SELECT
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    WHERE CreatedDate >= FromDate AND CreatedDate <= ToDate
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END$$
DELIMITER ;

-- =========================================================================
-- 6. BÀI TẬP 3.2: sp_GetTop5SellingProducts
-- =========================================================================
DROP PROCEDURE IF EXISTS sp_GetTop5SellingProducts;

DELIMITER $$
CREATE PROCEDURE sp_GetTop5SellingProducts()
BEGIN
    SELECT
        p.ProductId,
        p.Barcode,
        p.ProductName,
        SUM(od.Quantity) AS TotalQuantitySold,
        SUM(od.LineTotal) AS TotalRevenue
    FROM OrderDetails od
    INNER JOIN Products p ON od.ProductId = p.ProductId
    GROUP BY p.ProductId, p.Barcode, p.ProductName
    ORDER BY TotalQuantitySold DESC, TotalRevenue DESC
    LIMIT 5;
END$$
DELIMITER ;
