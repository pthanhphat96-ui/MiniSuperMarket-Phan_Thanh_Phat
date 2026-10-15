using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class phat1001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CashierId", "CreatedAt", "CustomerId", "Discount", "OrderCode", "PaymentMethod", "Status", "Subtotal", "Total" },
                values: new object[,]
                {
                    { 6, 8, new DateTime(2026, 10, 15, 8, 15, 0, 0, DateTimeKind.Unspecified), 1, 0m, "HD00006", "CASH", "PAID", 700000m, 700000m },
                    { 7, 9, new DateTime(2026, 10, 15, 9, 20, 0, 0, DateTimeKind.Unspecified), 2, 100000m, "HD00007", "CARD", "PAID", 2500000m, 2400000m },
                    { 8, 10, new DateTime(2026, 10, 15, 10, 10, 0, 0, DateTimeKind.Unspecified), 3, 0m, "HD00008", "MOMO", "PAID", 1800000m, 1800000m },
                    { 9, 11, new DateTime(2026, 10, 15, 11, 5, 0, 0, DateTimeKind.Unspecified), 4, 0m, "HD00009", "CASH", "PAID", 500000m, 500000m },
                    { 10, 12, new DateTime(2026, 10, 15, 13, 15, 0, 0, DateTimeKind.Unspecified), 5, 100000m, "HD00010", "BANK_TRANSFER", "PAID", 2100000m, 2000000m },
                    { 11, 13, new DateTime(2026, 10, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 6, 0m, "HD00011", "CASH", "PAID", 1200000m, 1200000m },
                    { 12, 14, new DateTime(2026, 10, 15, 15, 25, 0, 0, DateTimeKind.Unspecified), 7, 200000m, "HD00012", "CARD", "PAID", 3500000m, 3300000m },
                    { 13, 15, new DateTime(2026, 10, 15, 16, 10, 0, 0, DateTimeKind.Unspecified), 8, 0m, "HD00013", "MOMO", "PAID", 650000m, 650000m },
                    { 14, 8, new DateTime(2026, 10, 15, 17, 30, 0, 0, DateTimeKind.Unspecified), 9, 0m, "HD00014", "CASH", "PAID", 1450000m, 1450000m },
                    { 15, 9, new DateTime(2026, 10, 15, 19, 0, 0, 0, DateTimeKind.Unspecified), 10, 0m, "HD00015", "BANK_TRANSFER", "PAID", 2200000m, 2200000m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "LineTotal", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 11, 450000m, 6, 16, 1, 450000m },
                    { 12, 250000m, 6, 20, 1, 250000m },
                    { 13, 2500000m, 7, 17, 1, 2500000m },
                    { 14, 1800000m, 8, 18, 1, 1800000m },
                    { 15, 500000m, 9, 20, 2, 250000m },
                    { 16, 2100000m, 10, 21, 1, 2100000m },
                    { 17, 550000m, 11, 24, 1, 550000m },
                    { 18, 120000m, 11, 30, 1, 120000m },
                    { 19, 450000m, 11, 16, 1, 450000m },
                    { 20, 3500000m, 12, 22, 1, 3500000m },
                    { 21, 650000m, 13, 25, 1, 650000m },
                    { 22, 1450000m, 14, 28, 1, 1450000m },
                    { 23, 2200000m, 15, 27, 1, 2200000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 15);

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CashierId", "CreatedAt", "CustomerId", "Discount", "OrderCode", "PaymentMethod", "Status", "Subtotal", "Total" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2026, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), 1, 0m, "HD00001", "CASH", "PAID", 2950000m, 2950000m },
                    { 2, 9, new DateTime(2026, 1, 5, 9, 15, 0, 0, DateTimeKind.Unspecified), 2, 0m, "HD00002", "CARD", "PAID", 2300000m, 2300000m },
                    { 3, 10, new DateTime(2026, 1, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 6, 0m, "HD00003", "BANK_TRANSFER", "PAID", 14100000m, 14100000m },
                    { 4, 11, new DateTime(2026, 1, 12, 16, 45, 0, 0, DateTimeKind.Unspecified), 9, 0m, "HD00004", "CASH", "CANCELLED", 650000m, 650000m },
                    { 5, 12, new DateTime(2026, 1, 15, 8, 20, 0, 0, DateTimeKind.Unspecified), 13, 0m, "HD00005", "MOMO", "PAID", 4300000m, 4300000m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "LineTotal", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 450000m, 1, 16, 1, 450000m },
                    { 2, 2500000m, 1, 17, 1, 2500000m },
                    { 3, 1800000m, 2, 18, 1, 1800000m },
                    { 4, 500000m, 2, 20, 2, 250000m },
                    { 5, 12000000m, 3, 23, 1, 12000000m },
                    { 6, 2100000m, 3, 21, 1, 2100000m },
                    { 7, 650000m, 4, 25, 1, 650000m },
                    { 8, 1850000m, 5, 26, 1, 1850000m },
                    { 9, 2200000m, 5, 27, 1, 2200000m },
                    { 10, 250000m, 5, 20, 1, 250000m }
                });
        }
    }
}
