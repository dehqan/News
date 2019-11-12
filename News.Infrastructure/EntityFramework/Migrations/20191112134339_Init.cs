using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Infrastructure.EntityFramework.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    ModifyDateTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Lead = table.Column<string>(maxLength: 500, nullable: true),
                    Thumbnail = table.Column<string>(maxLength: 1000, nullable: true),
                    Image = table.Column<string>(maxLength: 1000, nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Link = table.Column<string>(maxLength: 1000, nullable: true),
                    PublishDateTime = table.Column<DateTime>(nullable: true),
                    PublishDateTimeMiliseconds = table.Column<int>(nullable: true),
                    PublishDateTimePersian = table.Column<string>(nullable: true),
                    PublishDateTimePersianFull = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCategories_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCategories_CategoryId",
                table: "ClientCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCategories_ClientId",
                table: "ClientCategories",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCategories");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
