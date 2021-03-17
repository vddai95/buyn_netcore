using Microsoft.EntityFrameworkCore.Migrations;

namespace byin_netcore_data.Migrations
{
    public partial class ProductCategoriesIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductCategoryName",
                table: "ProductCategories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductCategoryName",
                table: "ProductCategories",
                column: "ProductCategoryName",
                unique: true,
                filter: "[ProductCategoryName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductCategoryName",
                table: "ProductCategories");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCategoryName",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
