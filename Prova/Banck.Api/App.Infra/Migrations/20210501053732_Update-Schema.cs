using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infra.Migrations
{
    public partial class UpdateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assetss_Patrimonies_PatrimonyCode",
                table: "Assetss");

            migrationBuilder.DropTable(
                name: "Patrimonies");

            migrationBuilder.DropIndex(
                name: "IX_Assetss_PatrimonyCode",
                table: "Assetss");

            migrationBuilder.DropColumn(
                name: "PatrimonyCode",
                table: "Assetss");

            migrationBuilder.AddColumn<Guid>(
                name: "UserCode",
                table: "Assetss",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Assetss_UserCode",
                table: "Assetss",
                column: "UserCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Assetss_Users_UserCode",
                table: "Assetss",
                column: "UserCode",
                principalTable: "Users",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assetss_Users_UserCode",
                table: "Assetss");

            migrationBuilder.DropIndex(
                name: "IX_Assetss_UserCode",
                table: "Assetss");

            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "Assetss");

            migrationBuilder.AddColumn<Guid>(
                name: "PatrimonyCode",
                table: "Assetss",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Patrimonies",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SummarizedEquity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Update = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreate = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserUpdate = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrimonies", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Patrimonies_Users_UserCode",
                        column: x => x.UserCode,
                        principalTable: "Users",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assetss_PatrimonyCode",
                table: "Assetss",
                column: "PatrimonyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patrimonies_UserCode",
                table: "Patrimonies",
                column: "UserCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assetss_Patrimonies_PatrimonyCode",
                table: "Assetss",
                column: "PatrimonyCode",
                principalTable: "Patrimonies",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
