using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Infrastructure.EntityFramework.Migrations
{
    public partial class ExternalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Stories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Stories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stories_ClientId",
                table: "Stories",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Clients_ClientId",
                table: "Stories",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Clients_ClientId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Stories_ClientId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Stories");
        }
    }
}
