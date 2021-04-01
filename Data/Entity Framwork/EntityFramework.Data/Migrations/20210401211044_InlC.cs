using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Data.Migrations
{
    public partial class InlC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoMotiveRepair_ServiceReservations_ServiceReservationsId",
                table: "AutoMotiveRepair");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_ServiceReservations_ServiceReservationsId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_ServiceReservationsId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_AutoMotiveRepair_ServiceReservationsId",
                table: "AutoMotiveRepair");

            migrationBuilder.DropColumn(
                name: "ServiceReservationsId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "ServiceReservationsId",
                table: "AutoMotiveRepair");

            migrationBuilder.AddColumn<int>(
                name: "AutoMotiveRepairId",
                table: "ServiceReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "ServiceReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservations_AutoMotiveRepairId",
                table: "ServiceReservations",
                column: "AutoMotiveRepairId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservations_VehicleId",
                table: "ServiceReservations",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_AutoMotiveRepair_AutoMotiveRepairId",
                table: "ServiceReservations",
                column: "AutoMotiveRepairId",
                principalTable: "AutoMotiveRepair",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_Vehicle_VehicleId",
                table: "ServiceReservations",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_AutoMotiveRepair_AutoMotiveRepairId",
                table: "ServiceReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_Vehicle_VehicleId",
                table: "ServiceReservations");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReservations_AutoMotiveRepairId",
                table: "ServiceReservations");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReservations_VehicleId",
                table: "ServiceReservations");

            migrationBuilder.DropColumn(
                name: "AutoMotiveRepairId",
                table: "ServiceReservations");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "ServiceReservations");

            migrationBuilder.AddColumn<int>(
                name: "ServiceReservationsId",
                table: "Vehicle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceReservationsId",
                table: "AutoMotiveRepair",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_ServiceReservationsId",
                table: "Vehicle",
                column: "ServiceReservationsId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoMotiveRepair_ServiceReservationsId",
                table: "AutoMotiveRepair",
                column: "ServiceReservationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoMotiveRepair_ServiceReservations_ServiceReservationsId",
                table: "AutoMotiveRepair",
                column: "ServiceReservationsId",
                principalTable: "ServiceReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_ServiceReservations_ServiceReservationsId",
                table: "Vehicle",
                column: "ServiceReservationsId",
                principalTable: "ServiceReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
