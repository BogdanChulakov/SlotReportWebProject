using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSlotReports.Data.Migrations
{
    public partial class AddReportEntityAndDBset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_GamingHalls_GamingHallId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_SlotMachines_SlotMachineId",
                table: "Report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report",
                table: "Report");

            migrationBuilder.RenameTable(
                name: "Report",
                newName: "Reports");

            migrationBuilder.RenameIndex(
                name: "IX_Report_SlotMachineId",
                table: "Reports",
                newName: "IX_Reports_SlotMachineId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_IsDeleted",
                table: "Reports",
                newName: "IX_Reports_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Report_GamingHallId",
                table: "Reports",
                newName: "IX_Reports_GamingHallId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_GamingHalls_GamingHallId",
                table: "Reports",
                column: "GamingHallId",
                principalTable: "GamingHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_SlotMachines_SlotMachineId",
                table: "Reports",
                column: "SlotMachineId",
                principalTable: "SlotMachines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_GamingHalls_GamingHallId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_SlotMachines_SlotMachineId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "Report");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_SlotMachineId",
                table: "Report",
                newName: "IX_Report_SlotMachineId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_IsDeleted",
                table: "Report",
                newName: "IX_Report_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_GamingHallId",
                table: "Report",
                newName: "IX_Report_GamingHallId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report",
                table: "Report",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_GamingHalls_GamingHallId",
                table: "Report",
                column: "GamingHallId",
                principalTable: "GamingHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_SlotMachines_SlotMachineId",
                table: "Report",
                column: "SlotMachineId",
                principalTable: "SlotMachines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
