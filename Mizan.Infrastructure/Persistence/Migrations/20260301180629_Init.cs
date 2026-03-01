using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mizan.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NAV_Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    NAV_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    RIC_Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BBGTicker_Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Liquidity_SubscriptionFrequency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Liquidity_RedemptionFrequency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BasicData_ISINCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BasicData_ReutersCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BasicData_ListingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BasicData_ListedShares = table.Column<long>(type: "bigint", nullable: false),
                    BasicData_ParValue = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    BasicData_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    BasicData_SecurityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BasicData_Sector = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockDailyQuotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradedVolume = table.Column<long>(type: "bigint", nullable: false),
                    CouponPaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CouponNumber = table.Column<int>(type: "int", nullable: false),
                    TradingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PriceEarningRatio = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    DividendYield = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    TradedValue_Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    TradedValue_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ClosingPrice_Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    ClosingPrice_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CashDividends_Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    CashDividends_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    MarketCap_Amount = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    MarketCap_Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockDailyQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockDailyQuotes_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockIRContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockIRContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockIRContacts_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockDailyQuotes_StockId",
                table: "StockDailyQuotes",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockIRContacts_StockId",
                table: "StockIRContacts",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funds");

            migrationBuilder.DropTable(
                name: "StockDailyQuotes");

            migrationBuilder.DropTable(
                name: "StockIRContacts");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
