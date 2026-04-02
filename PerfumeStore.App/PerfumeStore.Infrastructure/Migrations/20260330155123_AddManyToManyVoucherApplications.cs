using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyVoucherApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentVouchers_PurchaseInvoices1",
                table: "PaymentVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentVouchers_Suppliers_SupplierId",
                table: "PaymentVouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVouchers_SalesInvoices1",
                table: "ReceiptVouchers");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVouchers_SalesInvoiceID",
                table: "ReceiptVouchers");

            migrationBuilder.DropIndex(
                name: "IX_PaymentVouchers_PurchaseInvoiceID",
                table: "PaymentVouchers");

            migrationBuilder.DropColumn(
                name: "SalesInvoiceID",
                table: "ReceiptVouchers");

            migrationBuilder.DropColumn(
                name: "PurchaseInvoiceID",
                table: "PaymentVouchers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpenseType",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "PaymentVoucherPurchaseInvoices",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentVoucherId = table.Column<long>(type: "bigint", nullable: false),
                    PurchaseInvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVoucherPurchaseInvoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentVoucherPurchaseInvoices_PaymentVouchers",
                        column: x => x.PaymentVoucherId,
                        principalTable: "PaymentVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentVoucherPurchaseInvoices_PurchaseInvoices",
                        column: x => x.PurchaseInvoiceId,
                        principalTable: "PurchaseInvoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptVoucherSalesInvoices",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptVoucherId = table.Column<long>(type: "bigint", nullable: false),
                    SalesInvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptVoucherSalesInvoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptVoucherSalesInvoices_ReceiptVouchers",
                        column: x => x.ReceiptVoucherId,
                        principalTable: "ReceiptVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptVoucherSalesInvoices_SalesInvoices",
                        column: x => x.SalesInvoiceId,
                        principalTable: "SalesInvoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucherPurchaseInvoices_PaymentVoucherId_PurchaseInvoiceId",
                table: "PaymentVoucherPurchaseInvoices",
                columns: new[] { "PaymentVoucherId", "PurchaseInvoiceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucherPurchaseInvoices_PurchaseInvoiceId",
                table: "PaymentVoucherPurchaseInvoices",
                column: "PurchaseInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucherSalesInvoices_ReceiptVoucherId_SalesInvoiceId",
                table: "ReceiptVoucherSalesInvoices",
                columns: new[] { "ReceiptVoucherId", "SalesInvoiceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucherSalesInvoices_SalesInvoiceId",
                table: "ReceiptVoucherSalesInvoices",
                column: "SalesInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentVouchers_Suppliers1",
                table: "PaymentVouchers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentVouchers_Suppliers1",
                table: "PaymentVouchers");

            migrationBuilder.DropTable(
                name: "PaymentVoucherPurchaseInvoices");

            migrationBuilder.DropTable(
                name: "ReceiptVoucherSalesInvoices");

            migrationBuilder.AddColumn<long>(
                name: "SalesInvoiceID",
                table: "ReceiptVouchers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "PaymentVouchers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "PurchaseInvoiceID",
                table: "PaymentVouchers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpenseType",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_SalesInvoiceID",
                table: "ReceiptVouchers",
                column: "SalesInvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_PurchaseInvoiceID",
                table: "PaymentVouchers",
                column: "PurchaseInvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentVouchers_PurchaseInvoices1",
                table: "PaymentVouchers",
                column: "PurchaseInvoiceID",
                principalTable: "PurchaseInvoices",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentVouchers_Suppliers_SupplierId",
                table: "PaymentVouchers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVouchers_SalesInvoices1",
                table: "ReceiptVouchers",
                column: "SalesInvoiceID",
                principalTable: "SalesInvoices",
                principalColumn: "ID");
        }
    }
}
