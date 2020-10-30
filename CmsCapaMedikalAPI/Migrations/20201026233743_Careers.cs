using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCapaMedikalAPI.Migrations
{
    public partial class Careers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Careers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CandidateFirstName = table.Column<string>(nullable: true),
                    CandidateLastName = table.Column<string>(nullable: true),
                    CandidateDescription = table.Column<string>(nullable: true),
                    CandidateDocument = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Careers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Careers");
        }
    }
}
