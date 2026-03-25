using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoneyAccountToPurchaseInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MoneyAccountId",
                table: "PurchaseInvoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoices_MoneyAccountId",
                table: "PurchaseInvoices",
                column: "MoneyAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_MoneyAccounts_MoneyAccountId",
                table: "PurchaseInvoices",
                column: "MoneyAccountId",
                principalTable: "MoneyAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoices_MoneyAccounts_MoneyAccountId",
                table: "PurchaseInvoices");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseInvoices_MoneyAccountId",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "MoneyAccountId",
                table: "PurchaseInvoices");
        }
    }
}
