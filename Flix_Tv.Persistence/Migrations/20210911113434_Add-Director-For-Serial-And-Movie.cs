using Microsoft.EntityFrameworkCore.Migrations;

namespace Flix_Tv.Persistence.Migrations
{
    public partial class AddDirectorForSerialAndMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Serials",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Director",
                table: "Serials");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movies");
        }
    }
}
