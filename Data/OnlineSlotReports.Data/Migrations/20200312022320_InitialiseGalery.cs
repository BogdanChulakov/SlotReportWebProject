namespace OnlineSlotReports.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InitialiseGalery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wins_GamingHalls_GamingHallid",
                table: "Wins");

            migrationBuilder.RenameColumn(
                name: "GamingHallid",
                table: "Wins",
                newName: "GamingHallId");

            migrationBuilder.RenameIndex(
                name: "IX_Wins_GamingHallid",
                table: "Wins",
                newName: "IX_Wins_GamingHallId");

            migrationBuilder.CreateTable(
                name: "Pics",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    GamingHallId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pics_GamingHalls_GamingHallId",
                        column: x => x.GamingHallId,
                        principalTable: "GamingHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pics_GamingHallId",
                table: "Pics",
                column: "GamingHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Pics_IsDeleted",
                table: "Pics",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Wins_GamingHalls_GamingHallId",
                table: "Wins",
                column: "GamingHallId",
                principalTable: "GamingHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wins_GamingHalls_GamingHallId",
                table: "Wins");

            migrationBuilder.DropTable(
                name: "Pics");

            migrationBuilder.RenameColumn(
                name: "GamingHallId",
                table: "Wins",
                newName: "GamingHallid");

            migrationBuilder.RenameIndex(
                name: "IX_Wins_GamingHallId",
                table: "Wins",
                newName: "IX_Wins_GamingHallid");

            migrationBuilder.AddForeignKey(
                name: "FK_Wins_GamingHalls_GamingHallid",
                table: "Wins",
                column: "GamingHallid",
                principalTable: "GamingHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
