using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class _3rdinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductDetails",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductDetails",
                table: "products");
        }
    }
}
