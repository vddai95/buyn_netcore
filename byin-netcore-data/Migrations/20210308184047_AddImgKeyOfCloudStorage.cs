using Microsoft.EntityFrameworkCore.Migrations;

namespace byin_netcore_data.Migrations
{
    public partial class AddImgKeyOfCloudStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CloudStorageKey",
                table: "FilePath",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilePath_CloudStorageKey",
                table: "FilePath",
                column: "CloudStorageKey",
                unique: true,
                filter: "[CloudStorageKey] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FilePath_CloudStorageKey",
                table: "FilePath");

            migrationBuilder.DropColumn(
                name: "CloudStorageKey",
                table: "FilePath");
        }
    }
}
