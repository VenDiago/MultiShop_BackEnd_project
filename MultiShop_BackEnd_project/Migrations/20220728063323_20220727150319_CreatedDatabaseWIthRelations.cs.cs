using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiShop_BackEnd_project.Migrations
{
    public partial class _20220727150319_CreatedDatabaseWIthRelationscs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Clothes",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Clothes");
        }
    }
}
