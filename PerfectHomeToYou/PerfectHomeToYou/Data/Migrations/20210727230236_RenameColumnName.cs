using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectHomeToYou.Data.Migrations
{
    public partial class RenameColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RentOrSell",
                table: "Apartments",
                newName: "RentOrSale");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RentOrSale",
                table: "Apartments",
                newName: "RentOrSell");
        }
    }
}
