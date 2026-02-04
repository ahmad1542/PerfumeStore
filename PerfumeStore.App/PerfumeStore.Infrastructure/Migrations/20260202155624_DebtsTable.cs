using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DebtsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debt_Persons_PersonPhone",
                table: "Debt");

            migrationBuilder.DropForeignKey(
                name: "FK_Debt_PurchaseInvoices_PurchaseInvoiceId",
                table: "Debt");

            migrationBuilder.DropForeignKey(
                name: "FK_Debt_SalesInvoices_SalesInvoiceId",
                table: "Debt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Debt",
                table: "Debt");

            migrationBuilder.RenameTable(
                name: "Debt",
                newName: "Debts");

            migrationBuilder.RenameIndex(
                name: "IX_Debt_SalesInvoiceId",
                table: "Debts",
                newName: "IX_Debts_SalesInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Debt_PurchaseInvoiceId",
                table: "Debts",
                newName: "IX_Debts_PurchaseInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Debt_PersonPhone",
                table: "Debts",
                newName: "IX_Debts_PersonPhone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Debts",
                table: "Debts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Persons_PersonPhone",
                table: "Debts",
                column: "PersonPhone",
                principalTable: "Persons",
                principalColumn: "Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_PurchaseInvoices_PurchaseInvoiceId",
                table: "Debts",
                column: "PurchaseInvoiceId",
                principalTable: "PurchaseInvoices",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_SalesInvoices_SalesInvoiceId",
                table: "Debts",
                column: "SalesInvoiceId",
                principalTable: "SalesInvoices",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Persons_SupplierPhone",
                table: "PurchaseInvoices",
                column: "SupplierPhone",
                principalTable: "Persons",
                principalColumn: "Phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Persons_PersonPhone",
                table: "Debts");

            migrationBuilder.DropForeignKey(
                name: "FK_Debts_PurchaseInvoices_PurchaseInvoiceId",
                table: "Debts");

            migrationBuilder.DropForeignKey(
                name: "FK_Debts_SalesInvoices_SalesInvoiceId",
                table: "Debts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoices_Persons_SupplierPhone",
                table: "PurchaseInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Debts",
                table: "Debts");

            migrationBuilder.RenameTable(
                name: "Debts",
                newName: "Debt");

            migrationBuilder.RenameIndex(
                name: "IX_Debts_SalesInvoiceId",
                table: "Debt",
                newName: "IX_Debt_SalesInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Debts_PurchaseInvoiceId",
                table: "Debt",
                newName: "IX_Debt_PurchaseInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Debts_PersonPhone",
                table: "Debt",
                newName: "IX_Debt_PersonPhone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Debt",
                table: "Debt",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Debt_Persons_PersonPhone",
                table: "Debt",
                column: "PersonPhone",
                principalTable: "Persons",
                principalColumn: "Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_Debt_PurchaseInvoices_PurchaseInvoiceId",
                table: "Debt",
                column: "PurchaseInvoiceId",
                principalTable: "PurchaseInvoices",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Debt_SalesInvoices_SalesInvoiceId",
                table: "Debt",
                column: "SalesInvoiceId",
                principalTable: "SalesInvoices",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Suppliers1",
                table: "PurchaseInvoices",
                column: "SupplierPhone",
                principalTable: "Persons",
                principalColumn: "Phone");
        }
    }
}
