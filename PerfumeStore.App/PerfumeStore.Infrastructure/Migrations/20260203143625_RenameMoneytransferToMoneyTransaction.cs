using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameMoneytransferToMoneyTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTransfers");

            migrationBuilder.CreateTable(
                name: "MoneyTransactions",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_MoneyTransactions_TransferDate"),
                    FromMoneyAccountID = table.Column<int>(type: "int", nullable: false),
                    ToMoneyAccountID = table.Column<int>(type: "int", nullable: false),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MoneyTransactions_MoneyAccounts",
                        column: x => x.ToMoneyAccountID,
                        principalTable: "MoneyAccounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MoneyTransactions_MoneyAccounts1",
                        column: x => x.FromMoneyAccountID,
                        principalTable: "MoneyAccounts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransactions_FromMoneyAccountID",
                table: "MoneyTransactions",
                column: "FromMoneyAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransactions_ToMoneyAccountID",
                table: "MoneyTransactions",
                column: "ToMoneyAccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTransactions");

            migrationBuilder.CreateTable(
                name: "MoneyTransactions",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromMoneyAccountID = table.Column<int>(type: "int", nullable: false),
                    ToMoneyAccountID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                        .Annotation("Relational:DefaultConstraintName", "DF_MoneyTransactions_TransferDate"),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MoneyTransactions_MoneyAccounts",
                        column: x => x.ToMoneyAccountID,
                        principalTable: "MoneyAccounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MoneyTransactions_MoneyAccounts1",
                        column: x => x.FromMoneyAccountID,
                        principalTable: "MoneyAccounts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransactions_FromMoneyAccountID",
                table: "MoneyTransfers",
                column: "FromMoneyAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransactions_ToMoneyAccountID",
                table: "MoneyTransfers",
                column: "ToMoneyAccountID");
        }
    }
}
