using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Data.Migrations
{
    public partial class InitialC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoMotiveRepairId",
                table: "ServiceReservations");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "ServiceReservations");

            migrationBuilder.RenameColumn(
                name: "ServiceBooked",
                table: "Vehicle",
                newName: "IsServiceBooked");

            migrationBuilder.RenameColumn(
                name: "DrivingBan",
                table: "Vehicle",
                newName: "IsDrivingBan");

            migrationBuilder.AddColumn<int>(
                name: "ServiceReservationsId",
                table: "Vehicle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AutoMotiveRepair",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AutoMotiveRepair",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AutoMotiveRepair",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AutoMotiveRepair",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AutoMotiveRepair",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationNumber",
                table: "AutoMotiveRepair",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "AutoMotiveRepair",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceReservationsId",
                table: "AutoMotiveRepair",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "AutoMotiveRepair",
                type: "nvarchar(max)",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Address",
                table: "AutoMotiveRepair");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AutoMotiveRepair");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AutoMotiveRepair");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AutoMotiveRepair");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AutoMotiveRepair");

            migrationBuilder.DropColumn(
                name: "OrganisationNumber",
                table: "AutoMotiveRepair");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AutoMotiveRepair");

            migrationBuilder.DropColumn(
                name: "ServiceReservationsId",
                table: "AutoMotiveRepair");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "AutoMotiveRepair");

            migrationBuilder.RenameColumn(
                name: "IsServiceBooked",
                table: "Vehicle",
                newName: "ServiceBooked");

            migrationBuilder.RenameColumn(
                name: "IsDrivingBan",
                table: "Vehicle",
                newName: "DrivingBan");

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
        }
    }
}
