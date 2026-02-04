using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MappingChangedFromTPHToTPC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Persons_PersonPhone",
                table: "Debts");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentVouchers_Persons_SupplierPhone",
                table: "PaymentVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVouchers_Customers1",
                table: "ReceiptVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoices_Customers1",
                table: "SalesInvoices");

            migrationBuilder.DropIndex(
                name: "IX_Debts_PersonPhone",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "sysdatetime()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Phone);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "sysdatetime()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Phone);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Debts_PersonPhone",
                table: "Debts",
                column: "PersonPhone");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentVouchers_Suppliers_SupplierPhone",
                table: "PaymentVouchers",
                column: "SupplierPhone",
                principalTable: "Suppliers",
                principalColumn: "Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Suppliers1",
                table: "PurchaseInvoices",
                column: "SupplierPhone",
                principalTable: "Suppliers",
                principalColumn: "Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVouchers_Customers1",
                table: "ReceiptVouchers",
                column: "CustomerPhone",
                principalTable: "Customers",
                principalColumn: "Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoices_Customers1",
                table: "SalesInvoices",
                column: "CustomerPhone",
                principalTable: "Customers",
                principalColumn: "Phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentVouchers_Suppliers_SupplierPhone",
                table: "PaymentVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoices_Suppliers1",
                table: "PurchaseInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVouchers_Customers1",
                table: "ReceiptVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoices_Customers1",
                table: "SalesInvoices");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Debts_PersonPhone",
                table: "Debts");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Persons",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Debts_PersonPhone",
                table: "Debts",
                column: "PersonPhone",
                unique: true,
                filter: "[PersonPhone] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Persons_PersonPhone",
                table: "Debts",
                column: "PersonPhone",
                principalTable: "Persons",
                principalColumn: "Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentVouchers_Persons_SupplierPhone",
                table: "PaymentVouchers",
                column: "SupplierPhone",
                principalTable: "Persons",
                principalColumn: "Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Suppliers1",
                table: "PurchaseInvoices",
                column: "SupplierPhone",
                principalTable: "Persons",
                principalColumn: "Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVouchers_Customers1",
                table: "ReceiptVouchers",
                column: "CustomerPhone",
                principalTable: "Persons",
                principalColumn: "Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoices_Customers1",
                table: "SalesInvoices",
                column: "CustomerPhone",
                principalTable: "Persons",
                principalColumn: "Phone");
        }
    }
}
