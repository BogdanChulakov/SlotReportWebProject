using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSlotReports.Data.Migrations
{
    public partial class DeleteColumnsFromReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_SlotMachines_SlotMachineId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_SlotMachineId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "EllInForDay",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "EllOutForDay",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "MechInForDay",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "MechOutForDay",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "SlotMachineId",
                table: "Reports");

            migrationBuilder.AddColumn<decimal>(
                name: "InForDay",
                table: "Reports",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OutForDay",
                table: "Reports",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InForDay",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "OutForDay",
                table: "Reports");

            migrationBuilder.AddColumn<decimal>(
                name: "EllInForDay",
                table: "Reports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EllOutForDay",
                table: "Reports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MechInForDay",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MechOutForDay",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SlotMachineId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SlotMachineId",
                table: "Reports",
                column: "SlotMachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_SlotMachines_SlotMachineId",
                table: "Reports",
                column: "SlotMachineId",
                principalTable: "SlotMachines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
