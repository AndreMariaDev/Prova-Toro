using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infra.Migrations
{
    public partial class AddFK_NEW : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BankAccounts_Code",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Patrimonies_Code",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCredentialss_Users_Code",
                table: "UsersCredentialss");

            migrationBuilder.AddColumn<Guid>(
                name: "UserCode",
                table: "UsersCredentialss",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserCode",
                table: "Patrimonies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserCode",
                table: "BankAccounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UsersCredentialss_UserCode",
                table: "UsersCredentialss",
                column: "UserCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patrimonies_UserCode",
                table: "Patrimonies",
                column: "UserCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_UserCode",
                table: "BankAccounts",
                column: "UserCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Users_UserCode",
                table: "BankAccounts",
                column: "UserCode",
                principalTable: "Users",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrimonies_Users_UserCode",
                table: "Patrimonies",
                column: "UserCode",
                principalTable: "Users",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCredentialss_Users_UserCode",
                table: "UsersCredentialss",
                column: "UserCode",
                principalTable: "Users",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Users_UserCode",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrimonies_Users_UserCode",
                table: "Patrimonies");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCredentialss_Users_UserCode",
                table: "UsersCredentialss");

            migrationBuilder.DropIndex(
                name: "IX_UsersCredentialss_UserCode",
                table: "UsersCredentialss");

            migrationBuilder.DropIndex(
                name: "IX_Patrimonies_UserCode",
                table: "Patrimonies");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_UserCode",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "UsersCredentialss");

            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "Patrimonies");

            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "BankAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BankAccounts_Code",
                table: "Users",
                column: "Code",
                principalTable: "BankAccounts",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Patrimonies_Code",
                table: "Users",
                column: "Code",
                principalTable: "Patrimonies",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCredentialss_Users_Code",
                table: "UsersCredentialss",
                column: "Code",
                principalTable: "Users",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
