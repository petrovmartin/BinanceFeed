using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BinanceFeed.Infrastructure.SQL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentPrice",
                table: "BinanceEvents",
                newName: "Weighted24Avg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weighted24Avg",
                table: "BinanceEvents",
                newName: "CurrentPrice");
        }
    }
}
