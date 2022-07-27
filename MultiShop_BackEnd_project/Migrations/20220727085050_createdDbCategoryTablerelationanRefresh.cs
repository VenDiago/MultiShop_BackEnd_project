using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiShop_BackEnd_project.Migrations
{
    public partial class createdDbCategoryTablerelationanRefresh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Clothes_ClothesId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothesImage_Clothes_ClothesId",
                table: "ClothesImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClothesImage",
                table: "ClothesImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "ClothesImage",
                newName: "ClothesImages");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_ClothesImage_ClothesId",
                table: "ClothesImages",
                newName: "IX_ClothesImages_ClothesId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ClothesId",
                table: "Categories",
                newName: "IX_Categories_ClothesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClothesImages",
                table: "ClothesImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Clothes_ClothesId",
                table: "Categories",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothesImages_Clothes_ClothesId",
                table: "ClothesImages",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Clothes_ClothesId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothesImages_Clothes_ClothesId",
                table: "ClothesImages");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClothesImages",
                table: "ClothesImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "ClothesImages",
                newName: "ClothesImage");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_ClothesImages_ClothesId",
                table: "ClothesImage",
                newName: "IX_ClothesImage_ClothesId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ClothesId",
                table: "Category",
                newName: "IX_Category_ClothesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClothesImage",
                table: "ClothesImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Clothes_ClothesId",
                table: "Category",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothesImage_Clothes_ClothesId",
                table: "ClothesImage",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
