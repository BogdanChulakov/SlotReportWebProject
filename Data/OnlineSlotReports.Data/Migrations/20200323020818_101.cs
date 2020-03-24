using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSlotReports.Data.Migrations
{
    public partial class _101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MechjIn",
                table: "MachineCounters");

            migrationBuilder.AddColumn<int>(
                name: "MechIn",
                table: "MachineCounters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MechIn",
                table: "MachineCounters");

            migrationBuilder.AddColumn<int>(
                name: "MechjIn",
                table: "MachineCounters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
