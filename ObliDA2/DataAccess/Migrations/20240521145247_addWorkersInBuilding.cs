using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addWorkersInBuilding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_MaintenancePersonnel_MaintenanceStaffId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_MaintenanceStaffId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "MaintenanceStaffId",
                table: "Buildings");

            migrationBuilder.CreateTable(
                name: "BuildingMaintenanceStaff",
                columns: table => new
                {
                    BuildingsId = table.Column<int>(type: "int", nullable: false),
                    WorkersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMaintenanceStaff", x => new { x.BuildingsId, x.WorkersId });
                    table.ForeignKey(
                        name: "FK_BuildingMaintenanceStaff_Buildings_BuildingsId",
                        column: x => x.BuildingsId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingMaintenanceStaff_MaintenancePersonnel_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "MaintenancePersonnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMaintenanceStaff_WorkersId",
                table: "BuildingMaintenanceStaff",
                column: "WorkersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingMaintenanceStaff");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceStaffId",
                table: "Buildings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_MaintenanceStaffId",
                table: "Buildings",
                column: "MaintenanceStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_MaintenancePersonnel_MaintenanceStaffId",
                table: "Buildings",
                column: "MaintenanceStaffId",
                principalTable: "MaintenancePersonnel",
                principalColumn: "Id");
        }
    }
}
