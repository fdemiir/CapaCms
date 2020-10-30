using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCapaMedikalAPI.Migrations
{
    public partial class Contents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ContentTitle = table.Column<string>(nullable: true),
                    ContentDescription = table.Column<string>(nullable: true),
                    ContentImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contents");
        }
    }
}
