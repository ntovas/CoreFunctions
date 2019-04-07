using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreFunctions.Data.Migrations
{
    public partial class AddImports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imports",
                table: "Functions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "References",
                table: "Functions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imports",
                table: "Functions");

            migrationBuilder.DropColumn(
                name: "References",
                table: "Functions");
        }
    }
}
