using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSlotReports.Data.Migrations
{
    public partial class AddUsersHalls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamingHalls_AspNetUsers_UserId",
                table: "GamingHalls");

            migrationBuilder.DropIndex(
                name: "IX_GamingHalls_UserId",
                table: "GamingHalls");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GamingHalls");

            migrationBuilder.CreateTable(
                name: "UsersHalls",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    GamingHallId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersHalls", x => new { x.UserId, x.GamingHallId });
                    table.ForeignKey(
                        name: "FK_UsersHalls_GamingHalls_GamingHallId",
                        column: x => x.GamingHallId,
                        principalTable: "GamingHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersHalls_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersHalls_GamingHallId",
                table: "UsersHalls",
                column: "GamingHallId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersHalls");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GamingHalls",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GamingHalls_UserId",
                table: "GamingHalls",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamingHalls_AspNetUsers_UserId",
                table: "GamingHalls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
