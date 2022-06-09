using Microsoft.EntityFrameworkCore.Migrations;

namespace AnonymousMessage.Migrations
{
    public partial class init111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "messeges",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "username",
                table: "messeges");
        }
    }
}
