using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangethePKofthepersonfromphonetoguidid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoices_Persons_SupplierPhone",
                table: "PurchaseInvoices");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_SalesInvoices_CustomerPhone",
                table: "SalesInvoices");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVouchers_CustomerPhone",
                table: "ReceiptVouchers");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseInvoices_SupplierPhone",
                table: "PurchaseInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_PaymentVouchers_SupplierPhone",
                table: "PaymentVouchers");

            migrationBuilder.DropIndex(
                name: "IX_Debts_PersonPhone",
                table: "Debts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "ReceiptVouchers");

            migrationBuilder.DropColumn(
                name: "SupplierPhone",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "SupplierPhone",
                table: "PaymentVouchers");

            migrationBuilder.DropColumn(
                name: "PersonPhone",
                table: "Debts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Suppliers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPaid",
                table: "SalesInvoices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "SalesInvoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "ReceiptVouchers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPaid",
                table: "PurchaseInvoices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "PurchaseInvoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "PaymentVouchers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Debts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Debts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Phone",
                table: "Suppliers",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_CustomerId",
                table: "SalesInvoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_CustomerId",
                table: "ReceiptVouchers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoices_SupplierId",
                table: "PurchaseInvoices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Phone",
                table: "Persons",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_SupplierId",
                table: "PaymentVouchers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Debts_PersonId",
                table: "Debts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Phone",
                table: "Customers",
                column: "Phone",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentVouchers_Suppliers_SupplierId",
                table: "PaymentVouchers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Suppliers1",
                table: "PurchaseInvoices",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVouchers_Customers1",
                table: "ReceiptVouchers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoices_Customers1",
                table: "SalesInvoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentVouchers_Suppliers_SupplierId",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_Phone",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_SalesInvoices_CustomerId",
                table: "SalesInvoices");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVouchers_CustomerId",
                table: "ReceiptVouchers");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseInvoices_SupplierId",
                table: "PurchaseInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_Phone",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_PaymentVouchers_SupplierId",
                table: "PaymentVouchers");

            migrationBuilder.DropIndex(
                name: "IX_Debts_PersonId",
                table: "Debts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Phone",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ReceiptVouchers");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "PaymentVouchers");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "AmountPaid",
                table: "SalesInvoices",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "SalesInvoices",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "ReceiptVouchers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AmountPaid",
                table: "PurchaseInvoices",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "SupplierPhone",
                table: "PurchaseInvoices",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SupplierPhone",
                table: "PaymentVouchers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Debts",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "PersonPhone",
                table: "Debts",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Phone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Phone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_CustomerPhone",
                table: "SalesInvoices",
                column: "CustomerPhone");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_CustomerPhone",
                table: "ReceiptVouchers",
                column: "CustomerPhone");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoices_SupplierPhone",
                table: "PurchaseInvoices",
                column: "SupplierPhone");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_SupplierPhone",
                table: "PaymentVouchers",
                column: "SupplierPhone");

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
    }
}
