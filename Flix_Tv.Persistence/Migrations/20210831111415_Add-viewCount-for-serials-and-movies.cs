using Microsoft.EntityFrameworkCore.Migrations;

namespace Flix_Tv.Persistence.Migrations
{
    public partial class AddviewCountforserialsandmovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ViewCount",
                table: "Serials",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ViewCount",
                table: "Movies",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Serials");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Movies");
        }
    }
}
