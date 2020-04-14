using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSlotReports.Data.Migrations
{
    public partial class addMessageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    Content = table.Column<string>(maxLength: 1000, nullable: false),
                    Readed = table.Column<bool>(nullable: false),
                    GaminhHallId = table.Column<string>(nullable: false),
                    GamingHallId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_GamingHalls_GamingHallId",
                        column: x => x.GamingHallId,
                        principalTable: "GamingHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_GamingHallId",
                table: "Message",
                column: "GamingHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_IsDeleted",
                table: "Message",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
