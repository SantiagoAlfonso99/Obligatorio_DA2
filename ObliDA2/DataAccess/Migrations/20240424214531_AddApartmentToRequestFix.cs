using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddApartmentToRequestFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Invitations");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DepartmentId",
                table: "Requests",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Apartments_DepartmentId",
                table: "Requests",
                column: "DepartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Apartments_DepartmentId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_DepartmentId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Requests");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Invitations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
