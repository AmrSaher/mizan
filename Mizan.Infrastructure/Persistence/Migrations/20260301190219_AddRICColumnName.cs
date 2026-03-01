using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mizan.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRICColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RIC_Value",
                table: "Funds",
                newName: "RICTicker_Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RICTicker_Value",
                table: "Funds",
                newName: "RIC_Value");
        }
    }
}
