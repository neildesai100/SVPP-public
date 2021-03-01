using Microsoft.EntityFrameworkCore.Migrations;

namespace SVPP.Migrations
{
    public partial class nonprofitFocusAreas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Focus_Area",
                table: "Nonprofit");

            migrationBuilder.AddColumn<string>(
                name: "FocusArea1",
                table: "Nonprofit",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Focus_Area2",
                table: "Nonprofit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Focus_Area3",
                table: "Nonprofit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Focus_Area1",
                table: "Nonprofit");

            migrationBuilder.DropColumn(
                name: "Focus_Area2",
                table: "Nonprofit");

            migrationBuilder.DropColumn(
                name: "Focus_Area3",
                table: "Nonprofit");

            migrationBuilder.AddColumn<string>(
                name: "Focus_Area",
                table: "Nonprofit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
