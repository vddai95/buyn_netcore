using Microsoft.EntityFrameworkCore.Migrations;

namespace byin_netcore_data.Migrations
{
    public partial class AddingRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderEntities_ProductId",
                table: "OrderEntities",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntities_Products_ProductId",
                table: "OrderEntities",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntities_Products_ProductId",
                table: "OrderEntities");

            migrationBuilder.DropIndex(
                name: "IX_OrderEntities_ProductId",
                table: "OrderEntities");
        }
    }
}
