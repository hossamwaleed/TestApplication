using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApplication.Migrations
{
    /// <inheritdoc />
    public partial class addProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productImage_Products_productid",
                table: "productImage");

            migrationBuilder.DropIndex(
                name: "IX_productImage_productid",
                table: "productImage");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "productImage",
                newName: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_productImage_ProductId",
                table: "productImage",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_productImage_Products_ProductId",
                table: "productImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productImage_Products_ProductId",
                table: "productImage");

            migrationBuilder.DropIndex(
                name: "IX_productImage_ProductId",
                table: "productImage");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "productImage",
                newName: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_productImage_productid",
                table: "productImage",
                column: "productid");

            migrationBuilder.AddForeignKey(
                name: "FK_productImage_Products_productid",
                table: "productImage",
                column: "productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
