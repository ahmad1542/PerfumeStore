using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetBrandDescriptionLengthToMax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BrandDescription",
                table: "Brands",
                type: "nvarchar(max)",
                fixedLength: true,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(250)",
                oldFixedLength: true,
                oldMaxLength: 250,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BrandDescription",
                table: "Brands",
                type: "nchar(250)",
                fixedLength: true,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldFixedLength: true,
                oldNullable: true);
        }
    }
}
