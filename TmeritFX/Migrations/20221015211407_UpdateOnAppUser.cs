using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TmeritFX.Migrations
{
    public partial class UpdateOnAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefLink",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefLink",
                table: "AspNetUsers");
        }
    }
}
