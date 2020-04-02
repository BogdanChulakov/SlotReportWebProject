using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSlotReports.Data.Migrations
{
    public partial class replaceUmageImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UmageUrl",
                table: "GamingHalls");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "GamingHalls",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "GamingHalls");

            migrationBuilder.AddColumn<string>(
                name: "UmageUrl",
                table: "GamingHalls",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
