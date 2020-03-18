namespace OnlineSlotReports.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DbBogdan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamingHalls",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    HallName = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamingHalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamingHalls_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    StartWorkDate = table.Column<DateTime>(nullable: false),
                    EndWorkDate = table.Column<DateTime>(nullable: true),
                    GamingHallId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_GamingHalls_GamingHallId",
                        column: x => x.GamingHallId,
                        principalTable: "GamingHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SlotMachines",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    LicenseNumber = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    NumberInHall = table.Column<int>(nullable: false),
                    GamingHallId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotMachines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlotMachines_GamingHalls_GamingHallId",
                        column: x => x.GamingHallId,
                        principalTable: "GamingHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MachineCounters",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ElIn = table.Column<decimal>(nullable: false),
                    ElOut = table.Column<decimal>(nullable: false),
                    MechjIn = table.Column<int>(nullable: false),
                    MechOut = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    SlotMachineId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineCounters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineCounters_SlotMachines_SlotMachineId",
                        column: x => x.SlotMachineId,
                        principalTable: "SlotMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wins",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    GamingHallid = table.Column<string>(nullable: true),
                    SlotMachineId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wins_GamingHalls_GamingHallid",
                        column: x => x.GamingHallid,
                        principalTable: "GamingHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wins_SlotMachines_SlotMachineId",
                        column: x => x.SlotMachineId,
                        principalTable: "SlotMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GamingHallId",
                table: "Employees",
                column: "GamingHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IsDeleted",
                table: "Employees",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_GamingHalls_IsDeleted",
                table: "GamingHalls",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_GamingHalls_UserId",
                table: "GamingHalls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineCounters_SlotMachineId",
                table: "MachineCounters",
                column: "SlotMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_SlotMachines_GamingHallId",
                table: "SlotMachines",
                column: "GamingHallId");

            migrationBuilder.CreateIndex(
                name: "IX_SlotMachines_IsDeleted",
                table: "SlotMachines",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Wins_GamingHallid",
                table: "Wins",
                column: "GamingHallid");

            migrationBuilder.CreateIndex(
                name: "IX_Wins_IsDeleted",
                table: "Wins",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Wins_SlotMachineId",
                table: "Wins",
                column: "SlotMachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "MachineCounters");

            migrationBuilder.DropTable(
                name: "Wins");

            migrationBuilder.DropTable(
                name: "SlotMachines");

            migrationBuilder.DropTable(
                name: "GamingHalls");
        }
    }
}
