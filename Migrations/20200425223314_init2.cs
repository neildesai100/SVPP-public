using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SVPP.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nonprofit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Organization_Name = table.Column<string>(nullable: false),
                    Focus_Area = table.Column<string>(nullable: false),
                    Date_Founded = table.Column<DateTime>(nullable: false),
                    Phone_Number = table.Column<string>(nullable: false),
                    Street_Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(maxLength: 2, nullable: false),
                    Zip_Code = table.Column<string>(maxLength: 5, nullable: false),
                    Website = table.Column<string>(nullable: true),
                    Executive_Name = table.Column<string>(nullable: false),
                    Contact_Name = table.Column<string>(nullable: false),
                    Contact_Title = table.Column<string>(nullable: false),
                    Contact_Email = table.Column<string>(nullable: false),
                    Contact_Phone = table.Column<string>(nullable: false),
                    Facebook_Id = table.Column<string>(nullable: true),
                    Twitter_Id = table.Column<string>(nullable: true),
                    Instagram_Id = table.Column<string>(nullable: true),
                    Mission = table.Column<string>(nullable: false),
                    Programs = table.Column<string>(nullable: false),
                    Challenges = table.Column<string>(nullable: false),
                    Tax_Status = table.Column<string>(nullable: false),
                    Tax_Id = table.Column<string>(nullable: true),
                    Skill_1 = table.Column<string>(nullable: false),
                    Skill_2 = table.Column<string>(nullable: true),
                    Skill_3 = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nonprofit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(nullable: false),
                    Last_Name = table.Column<string>(nullable: false),
                    Phone_Number = table.Column<string>(nullable: false),
                    Street_Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(maxLength: 2, nullable: false),
                    Zip_Code = table.Column<string>(maxLength: 5, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Twitter_Id = table.Column<string>(nullable: true),
                    Linkedin_Id = table.Column<string>(nullable: false),
                    Employer = table.Column<string>(nullable: false),
                    Job_Title = table.Column<string>(nullable: false),
                    Bio = table.Column<string>(nullable: false),
                    Skill_1 = table.Column<string>(nullable: false),
                    Skill_2 = table.Column<string>(nullable: true),
                    Skill_3 = table.Column<string>(nullable: true),
                    Nonprofit_Preference = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nonprofit");

            migrationBuilder.DropTable(
                name: "Partner");
        }
    }
}
