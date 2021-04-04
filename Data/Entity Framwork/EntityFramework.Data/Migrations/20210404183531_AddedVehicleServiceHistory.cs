using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Data.Migrations
{
    public partial class AddedVehicleServiceHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleServiceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    AutoMotiveRepairId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleServiceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleServiceHistory_AutoMotiveRepair_AutoMotiveRepairId",
                        column: x => x.AutoMotiveRepairId,
                        principalTable: "AutoMotiveRepair",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleServiceHistory_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServiceHistory_AutoMotiveRepairId",
                table: "VehicleServiceHistory",
                column: "AutoMotiveRepairId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServiceHistory_VehicleId",
                table: "VehicleServiceHistory",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleServiceHistory");
        }
    }
}
