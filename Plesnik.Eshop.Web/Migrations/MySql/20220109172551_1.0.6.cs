using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Plesnik.Eshop.Web.Migrations.MySql
{
    public partial class _106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRelationEntry_ProductRelation_ProductRelationId",
                table: "ProductRelationEntry");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "ProductRelation");

            migrationBuilder.RenameColumn(
                name: "ProductRelationId",
                table: "ProductRelationEntry",
                newName: "RelatedProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRelationEntry_ProductRelationId",
                table: "ProductRelationEntry",
                newName: "IX_ProductRelationEntry_RelatedProductId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductRelationEntry",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductRelationEntry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductRelationEntry",
                table: "ProductRelationEntry",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelationEntry_ProductId",
                table: "ProductRelationEntry",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRelationEntry_ProductItem_ProductId",
                table: "ProductRelationEntry",
                column: "ProductId",
                principalTable: "ProductItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRelationEntry_ProductItem_RelatedProductId",
                table: "ProductRelationEntry",
                column: "RelatedProductId",
                principalTable: "ProductItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRelationEntry_ProductItem_ProductId",
                table: "ProductRelationEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRelationEntry_ProductItem_RelatedProductId",
                table: "ProductRelationEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductRelationEntry",
                table: "ProductRelationEntry");

            migrationBuilder.DropIndex(
                name: "IX_ProductRelationEntry_ProductId",
                table: "ProductRelationEntry");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductRelationEntry");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductRelationEntry");

            migrationBuilder.RenameColumn(
                name: "RelatedProductId",
                table: "ProductRelationEntry",
                newName: "ProductRelationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRelationEntry_RelatedProductId",
                table: "ProductRelationEntry",
                newName: "IX_ProductRelationEntry_ProductRelationId");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "ProductRelation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRelationEntry_ProductRelation_ProductRelationId",
                table: "ProductRelationEntry",
                column: "ProductRelationId",
                principalTable: "ProductRelation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
