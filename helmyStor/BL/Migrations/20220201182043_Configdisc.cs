using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace helmyStor.Migrations
{
    public partial class Configdisc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_products_ProductId",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "Discount");

            migrationBuilder.RenameIndex(
                name: "IX_Discounts_ProductId",
                table: "Discount",
                newName: "IX_Discount_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                table: "Discount",
                column: "ProductDiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_products_ProductId",
                table: "Discount",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_products_ProductId",
                table: "Discount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                table: "Discount");

            migrationBuilder.RenameTable(
                name: "Discount",
                newName: "Discounts");

            migrationBuilder.RenameIndex(
                name: "IX_Discount_ProductId",
                table: "Discounts",
                newName: "IX_Discounts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "ProductDiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_products_ProductId",
                table: "Discounts",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
