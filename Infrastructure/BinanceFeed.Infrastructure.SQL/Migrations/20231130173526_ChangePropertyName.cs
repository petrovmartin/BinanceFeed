using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BinanceFeed.Infrastructure.SQL.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TickerName",
                table: "BinanceStreamRecords",
                newName: "Symbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "BinanceStreamRecords",
                newName: "TickerName");
        }
    }
}
