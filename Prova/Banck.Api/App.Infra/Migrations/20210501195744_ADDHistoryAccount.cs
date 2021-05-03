using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infra.Migrations
{
    public partial class ADDHistoryAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccountHistories",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankAccountCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeMoving = table.Column<int>(type: "int", nullable: false),
                    AmountMoved = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreate = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Update = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpdate = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountHistories", x => x.Code);
                    table.ForeignKey(
                        name: "FK_BankAccountHistories_BankAccounts_BankAccountCode",
                        column: x => x.BankAccountCode,
                        principalTable: "BankAccounts",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountHistories_BankAccountCode",
                table: "BankAccountHistories",
                column: "BankAccountCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountHistories");
        }
    }
}
