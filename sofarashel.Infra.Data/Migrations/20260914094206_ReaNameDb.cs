using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sofarashel.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReaNameDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttributValue",
                table: "AttributeFeatures",
                newName: "AttributeValue");

            migrationBuilder.RenameColumn(
                name: "AttributTitle",
                table: "AttributeFeatures",
                newName: "AttributeTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttributeValue",
                table: "AttributeFeatures",
                newName: "AttributValue");

            migrationBuilder.RenameColumn(
                name: "AttributeTitle",
                table: "AttributeFeatures",
                newName: "AttributTitle");
        }
    }
}
