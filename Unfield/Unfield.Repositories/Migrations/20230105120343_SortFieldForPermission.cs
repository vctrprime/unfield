using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class SortFieldForPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sort",
                schema: "accounts",
                table: "permission_group",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sort",
                schema: "accounts",
                table: "permission",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sort",
                schema: "accounts",
                table: "permission_group");

            migrationBuilder.DropColumn(
                name: "sort",
                schema: "accounts",
                table: "permission");
        }
    }
}
