using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSlotReports.Data.Migrations
{
    public partial class removeError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GaminhHallId",
                table: "Message");

            migrationBuilder.AlterColumn<string>(
                name: "GamingHallId",
                table: "Message",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GamingHallId",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "GaminhHallId",
                table: "Message",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
