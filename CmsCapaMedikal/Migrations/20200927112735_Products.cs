using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCapaMedikal.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductArea = table.Column<string>(nullable: true),
                    ProductClass = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    ProductBottomBrand = table.Column<string>(nullable: true),
                    ProductImage = table.Column<string>(nullable: true),
                    ProductCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
