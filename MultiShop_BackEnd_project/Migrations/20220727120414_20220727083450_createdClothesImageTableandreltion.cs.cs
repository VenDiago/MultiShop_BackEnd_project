using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiShop_BackEnd_project.Migrations
{
    public partial class _20220727083450_createdClothesImageTableandreltioncs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ClothesImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ClothesImages");
        }
    }
}
