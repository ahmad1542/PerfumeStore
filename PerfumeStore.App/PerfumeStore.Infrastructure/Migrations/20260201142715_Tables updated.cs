using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tablesupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BrandDescription = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Customers_CreatedAt"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerPhone);
                });

            migrationBuilder.CreateTable(
                name: "MoneyAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyAccounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Suppliers_CreatedAt")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierPhone);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoices",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesInvoices_InvoiceDate"),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_Customers1",
                        column: x => x.CustomerPhone,
                        principalTable: "Customers",
                        principalColumn: "CustomerPhone");
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Expenses_ExpenseDate"),
                    Type = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MoneyAccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Expenses_MoneyAccounts1",
                        column: x => x.MoneyAccountID,
                        principalTable: "MoneyAccounts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MoneyTransfers",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_MoneyTransfers_TransferDate"),
                    FromMoneyAccountID = table.Column<int>(type: "int", nullable: false),
                    ToMoneyAccountID = table.Column<int>(type: "int", nullable: false),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransfers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MoneyTransfers_MoneyAccounts",
                        column: x => x.ToMoneyAccountID,
                        principalTable: "MoneyAccounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MoneyTransfers_MoneyAccounts1",
                        column: x => x.FromMoneyAccountID,
                        principalTable: "MoneyAccounts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinStock = table.Column<int>(type: "int", nullable: true),
                    BrandID = table.Column<int>(type: "int", nullable: true),
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Brands1",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories1",
                        column: x => x.ProductCategoryID,
                        principalTable: "ProductCategories",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoices",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_PurchaseInvoices_InvoiceDate"),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SupplierPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoices_Suppliers1",
                        column: x => x.SupplierPhone,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierPhone");
                });

            migrationBuilder.CreateTable(
                name: "ReceiptVouchers",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_ReceiptVouchers_ReceiptVoucherDate"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SalesInvoiceID = table.Column<long>(type: "bigint", nullable: true),
                    MoneyAccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Customers1",
                        column: x => x.CustomerPhone,
                        principalTable: "Customers",
                        principalColumn: "CustomerPhone");
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_MoneyAccounts1",
                        column: x => x.MoneyAccountID,
                        principalTable: "MoneyAccounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_SalesInvoices1",
                        column: x => x.SalesInvoiceID,
                        principalTable: "SalesInvoices",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Inventory_Products1",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceItems",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesInvoiceID = table.Column<long>(type: "bigint", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceItems_Products1",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesInvoiceItems_SalesInvoices1",
                        column: x => x.SalesInvoiceID,
                        principalTable: "SalesInvoices",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PaymentVouchers",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_PaymentVouchers_PaymentVoucherDate"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SupplierPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PurchaseInvoiceID = table.Column<long>(type: "bigint", nullable: true),
                    MoneyAccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_MoneyAccounts1",
                        column: x => x.MoneyAccountID,
                        principalTable: "MoneyAccounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_PurchaseInvoices1",
                        column: x => x.PurchaseInvoiceID,
                        principalTable: "PurchaseInvoices",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Suppliers1",
                        column: x => x.SupplierPhone,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierPhone");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoiceItems",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseInvoiceID = table.Column<long>(type: "bigint", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoiceItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceItems_Products1",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceItems_PurchaseInvoices1",
                        column: x => x.PurchaseInvoiceID,
                        principalTable: "PurchaseInvoices",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_MoneyAccountID",
                table: "Expenses",
                column: "MoneyAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransfers_FromMoneyAccountID",
                table: "MoneyTransfers",
                column: "FromMoneyAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransfers_ToMoneyAccountID",
                table: "MoneyTransfers",
                column: "ToMoneyAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_MoneyAccountID",
                table: "PaymentVouchers",
                column: "MoneyAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_PurchaseInvoiceID",
                table: "PaymentVouchers",
                column: "PurchaseInvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_SupplierPhone",
                table: "PaymentVouchers",
                column: "SupplierPhone");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandID",
                table: "Products",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceItems_ProductID",
                table: "PurchaseInvoiceItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceItems_PurchaseInvoiceID",
                table: "PurchaseInvoiceItems",
                column: "PurchaseInvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoices_SupplierPhone",
                table: "PurchaseInvoices",
                column: "SupplierPhone");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_CustomerPhone",
                table: "ReceiptVouchers",
                column: "CustomerPhone");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_MoneyAccountID",
                table: "ReceiptVouchers",
                column: "MoneyAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_SalesInvoiceID",
                table: "ReceiptVouchers",
                column: "SalesInvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceItems_ProductID",
                table: "SalesInvoiceItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceItems_SalesInvoiceID",
                table: "SalesInvoiceItems",
                column: "SalesInvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_CustomerPhone",
                table: "SalesInvoices",
                column: "CustomerPhone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "MoneyTransfers");

            migrationBuilder.DropTable(
                name: "PaymentVouchers");

            migrationBuilder.DropTable(
                name: "PurchaseInvoiceItems");

            migrationBuilder.DropTable(
                name: "ReceiptVouchers");

            migrationBuilder.DropTable(
                name: "SalesInvoiceItems");

            migrationBuilder.DropTable(
                name: "PurchaseInvoices");

            migrationBuilder.DropTable(
                name: "MoneyAccounts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SalesInvoices");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
