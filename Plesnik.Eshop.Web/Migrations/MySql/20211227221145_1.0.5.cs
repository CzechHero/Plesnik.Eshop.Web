using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Plesnik.Eshop.Web.Migrations.MySql
{
    public partial class _105 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RelatedProductId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    DateTimeCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRelation_ProductItem_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRelation_ProductItem_RelatedProductId",
                        column: x => x.RelatedProductId,
                        principalTable: "ProductItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductRelationEntry",
                columns: table => new
                {
                    ProductRelationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ProductRelationEntry_ProductRelation_ProductRelationId",
                        column: x => x.ProductRelationId,
                        principalTable: "ProductRelation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRelationEntry_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_ProductId",
                table: "ProductRelation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_RelatedProductId",
                table: "ProductRelation",
                column: "RelatedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelationEntry_ProductRelationId",
                table: "ProductRelationEntry",
                column: "ProductRelationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelationEntry_UserId",
                table: "ProductRelationEntry",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRelationEntry");

            migrationBuilder.DropTable(
                name: "ProductRelation");
        }
    }
}
