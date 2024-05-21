using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class newFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Manager_BuildingManagerId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenancePersonnel_Buildings_AssociatedBuildingId",
                table: "MaintenancePersonnel");

            migrationBuilder.DropIndex(
                name: "IX_MaintenancePersonnel_AssociatedBuildingId",
                table: "MaintenancePersonnel");

            migrationBuilder.DropColumn(
                name: "AssociatedBuildingId",
                table: "MaintenancePersonnel");

            migrationBuilder.DropColumn(
                name: "ConstructionCompany",
                table: "Buildings");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingManagerId",
                table: "Buildings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceStaffId",
                table: "Buildings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConstructionCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAdmin_ConstructionCompanies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "ConstructionCompanies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CompanyId",
                table: "Buildings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_MaintenanceStaffId",
                table: "Buildings",
                column: "MaintenanceStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAdmin_CompanyId",
                table: "CompanyAdmin",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ConstructionCompanies_CompanyId",
                table: "Buildings",
                column: "CompanyId",
                principalTable: "ConstructionCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_MaintenancePersonnel_MaintenanceStaffId",
                table: "Buildings",
                column: "MaintenanceStaffId",
                principalTable: "MaintenancePersonnel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Manager_BuildingManagerId",
                table: "Buildings",
                column: "BuildingManagerId",
                principalTable: "Manager",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ConstructionCompanies_CompanyId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_MaintenancePersonnel_MaintenanceStaffId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Manager_BuildingManagerId",
                table: "Buildings");

            migrationBuilder.DropTable(
                name: "CompanyAdmin");

            migrationBuilder.DropTable(
                name: "ConstructionCompanies");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_CompanyId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_MaintenanceStaffId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "MaintenanceStaffId",
                table: "Buildings");

            migrationBuilder.AddColumn<int>(
                name: "AssociatedBuildingId",
                table: "MaintenancePersonnel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BuildingManagerId",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConstructionCompany",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenancePersonnel_AssociatedBuildingId",
                table: "MaintenancePersonnel",
                column: "AssociatedBuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Manager_BuildingManagerId",
                table: "Buildings",
                column: "BuildingManagerId",
                principalTable: "Manager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenancePersonnel_Buildings_AssociatedBuildingId",
                table: "MaintenancePersonnel",
                column: "AssociatedBuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
