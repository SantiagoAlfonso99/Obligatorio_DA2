using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBuildingFromRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Buildings_BuildingAssociatedId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_BuildingAssociatedId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "BuildingAssociatedId",
                table: "Requests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildingAssociatedId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_BuildingAssociatedId",
                table: "Requests",
                column: "BuildingAssociatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Buildings_BuildingAssociatedId",
                table: "Requests",
                column: "BuildingAssociatedId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
