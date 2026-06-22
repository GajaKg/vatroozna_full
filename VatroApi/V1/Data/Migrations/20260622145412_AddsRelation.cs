using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VatroApi.V1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Controls_ClientId",
                table: "Controls",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Controls_Clients_ClientId",
                table: "Controls",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Controls_Clients_ClientId",
                table: "Controls");

            migrationBuilder.DropIndex(
                name: "IX_Controls_ClientId",
                table: "Controls");
        }
    }
}
