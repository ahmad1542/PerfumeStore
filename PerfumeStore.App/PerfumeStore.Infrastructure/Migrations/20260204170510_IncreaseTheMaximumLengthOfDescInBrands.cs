using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseTheMaximumLengthOfDescInBrands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<string>(
                name: "BrandDescription",
                table: "Brands",
                type: "nvarchar(250)",
                fixedLength: true,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyTransactions_MoneyAccounts",
                table: "MoneyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_MoneyTransactions_MoneyAccounts1",
                table: "MoneyTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "MoneyTransactions",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "(sysdatetime())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "(sysdatetime())")
                .Annotation("Relational:DefaultConstraintName", "DF_MoneyTransfers_TransferDate")
                .OldAnnotation("Relational:DefaultConstraintName", "DF_MoneyTransactions_TransferDate");

            migrationBuilder.AlterColumn<string>(
                name: "BrandDescription",
                table: "Brands",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(250)",
                oldFixedLength: true,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyTransfers_MoneyAccounts",
                table: "MoneyTransactions",
                column: "ToMoneyAccountID",
                principalTable: "MoneyAccounts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyTransfers_MoneyAccounts1",
                table: "MoneyTransactions",
                column: "FromMoneyAccountID",
                principalTable: "MoneyAccounts",
                principalColumn: "ID");
        }
    }
}
