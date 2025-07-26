using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITSenseAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedWithStaticDate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "InventoryOutputs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "InventoryOutputs");
        }
    }
}
