using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectHomeToYou.Data.Migrations
{
    public partial class ConnectionCityWithNeighborhood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Neighborhoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhoods_CityId",
                table: "Neighborhoods",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Neighborhoods_Cities_CityId",
                table: "Neighborhoods",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Neighborhoods_Cities_CityId",
                table: "Neighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_Neighborhoods_CityId",
                table: "Neighborhoods");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Neighborhoods");
        }
    }
}
