using Microsoft.EntityFrameworkCore.Migrations;

namespace Flix_Tv.Persistence.Migrations
{
    public partial class AddParentIdformovieCommentserialCommentepisodeComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "SerialEpisodeComments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "SerialComments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "MovieComments",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "SerialEpisodeComments");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "SerialComments");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "MovieComments");
        }
    }
}
