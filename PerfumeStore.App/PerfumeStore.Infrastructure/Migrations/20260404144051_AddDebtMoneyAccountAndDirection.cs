using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDebtMoneyAccountAndDirection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Direction",
                table: "Debts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MoneyAccountId",
                table: "Debts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Debts_MoneyAccountId",
                table: "Debts",
                column: "MoneyAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_MoneyAccounts_MoneyAccountId",
                table: "Debts",
                column: "MoneyAccountId",
                principalTable: "MoneyAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_MoneyAccounts_MoneyAccountId",
                table: "Debts");

            migrationBuilder.DropIndex(
                name: "IX_Debts_MoneyAccountId",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "MoneyAccountId",
                table: "Debts");
        }
    }
}
