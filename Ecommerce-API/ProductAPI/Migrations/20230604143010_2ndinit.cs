using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class _2ndinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "catagoryId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_catagoryId",
                table: "products",
                column: "catagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_catagories_catagoryId",
                table: "products",
                column: "catagoryId",
                principalTable: "catagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_catagories_catagoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_catagoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "catagoryId",
                table: "products");
        }
    }
}
