using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sofarashel.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDbNone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "FabricType",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ProductDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ImageByte",
                table: "CategoryImages",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageByte",
                table: "CategoryImages");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ProductDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FabricType",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "Categories",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "Categories",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "Categories",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
