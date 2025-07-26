using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITSenseAPI.Migrations
{
    /// <inheritdoc />
    public partial class ImplementingRepositories2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "InventoryOutputs",
                newName: "StockQuantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockQuantity",
                table: "InventoryOutputs",
                newName: "Quantity");
        }
    }
}
