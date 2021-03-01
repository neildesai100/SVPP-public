using Microsoft.EntityFrameworkCore.Migrations;

namespace SVPP.Migrations
{
    public partial class severalPartnerPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nonprofit_Preference",
                table: "Partner");

            migrationBuilder.AddColumn<string>(
                name: "Preference_1",
                table: "Partner",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Preference_2",
                table: "Partner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Preference_3",
                table: "Partner",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preference_1",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "Preference_2",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "Preference_3",
                table: "Partner");

            migrationBuilder.AddColumn<string>(
                name: "Nonprofit_Preference",
                table: "Partner",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
