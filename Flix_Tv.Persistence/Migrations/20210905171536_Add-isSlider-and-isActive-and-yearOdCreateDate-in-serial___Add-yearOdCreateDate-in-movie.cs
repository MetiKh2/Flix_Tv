using Microsoft.EntityFrameworkCore.Migrations;

namespace Flix_Tv.Persistence.Migrations
{
    public partial class AddisSliderandisActiveandyearOdCreateDateinserial___AddyearOdCreateDateinmovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Serials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSlider",
                table: "Serials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "YearOfCreateDate",
                table: "Serials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearOfCreateDate",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Serials");

            migrationBuilder.DropColumn(
                name: "IsSlider",
                table: "Serials");

            migrationBuilder.DropColumn(
                name: "YearOfCreateDate",
                table: "Serials");

            migrationBuilder.DropColumn(
                name: "YearOfCreateDate",
                table: "Movies");
        }
    }
}
