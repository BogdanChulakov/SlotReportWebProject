using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSlotReports.Data.Migrations
{
    public partial class addDbSetMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_GamingHalls_GamingHallId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Message_IsDeleted",
                table: "Messages",
                newName: "IX_Messages_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Message_GamingHallId",
                table: "Messages",
                newName: "IX_Messages_GamingHallId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_GamingHalls_GamingHallId",
                table: "Messages",
                column: "GamingHallId",
                principalTable: "GamingHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_GamingHalls_GamingHallId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_IsDeleted",
                table: "Message",
                newName: "IX_Message_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_GamingHallId",
                table: "Message",
                newName: "IX_Message_GamingHallId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_GamingHalls_GamingHallId",
                table: "Message",
                column: "GamingHallId",
                principalTable: "GamingHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
