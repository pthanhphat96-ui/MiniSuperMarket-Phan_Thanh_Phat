using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class _9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CashierUsername",
                table: "Orders",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "FinalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RewardPoints",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2732), 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2747), 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 8,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2750), 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 9,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2753), 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 10,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2755), 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 11,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2758), 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 12,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2760), 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 13,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2809), 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 14,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2812), 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 15,
                columns: new[] { "CashierUsername", "CreatedDate", "FinalAmount", "RewardPoints", "TotalItems" },
                values: new object[] { "", new DateTime(2026, 10, 28, 15, 23, 1, 265, DateTimeKind.Local).AddTicks(2814), 0m, 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashierUsername",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FinalAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RewardPoints",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "Orders");
        }
    }
}
