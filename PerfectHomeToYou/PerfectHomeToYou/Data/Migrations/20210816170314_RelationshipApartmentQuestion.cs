using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectHomeToYou.Data.Migrations
{
    public partial class RelationshipApartmentQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ApartmentId",
                table: "Questions",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Apartments_ApartmentId",
                table: "Questions",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Apartments_ApartmentId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ApartmentId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Questions");
        }
    }
}
