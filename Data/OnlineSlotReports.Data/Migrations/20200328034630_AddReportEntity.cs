using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSlotReports.Data.Migrations
{
    public partial class AddReportEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    EllInForDay = table.Column<decimal>(nullable: false),
                    EllOutForDay = table.Column<decimal>(nullable: false),
                    MechInForDay = table.Column<int>(nullable: false),
                    MechOutForDay = table.Column<int>(nullable: false),
                    SlotMachineId = table.Column<string>(nullable: false),
                    GamingHallId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_GamingHalls_GamingHallId",
                        column: x => x.GamingHallId,
                        principalTable: "GamingHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_SlotMachines_SlotMachineId",
                        column: x => x.SlotMachineId,
                        principalTable: "SlotMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_GamingHallId",
                table: "Report",
                column: "GamingHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_IsDeleted",
                table: "Report",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Report_SlotMachineId",
                table: "Report",
                column: "SlotMachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report");
        }
    }
}
