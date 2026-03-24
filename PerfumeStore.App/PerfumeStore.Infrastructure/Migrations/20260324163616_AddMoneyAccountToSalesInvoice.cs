using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoneyAccountToSalesInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MoneyAccountId",
                table: "SalesInvoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_MoneyAccountId",
                table: "SalesInvoices",
                column: "MoneyAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoices_MoneyAccounts_MoneyAccountId",
                table: "SalesInvoices",
                column: "MoneyAccountId",
                principalTable: "MoneyAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoices_MoneyAccounts_MoneyAccountId",
                table: "SalesInvoices");

            migrationBuilder.DropIndex(
                name: "IX_SalesInvoices_MoneyAccountId",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "MoneyAccountId",
                table: "SalesInvoices");
        }
    }
}
