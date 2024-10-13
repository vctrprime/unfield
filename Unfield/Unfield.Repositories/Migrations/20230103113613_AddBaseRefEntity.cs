using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseRefEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                schema: "accounts",
                table: "role_stadium");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "accounts",
                table: "role_stadium");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "accounts",
                table: "role_permission");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "accounts",
                table: "role_permission");

            migrationBuilder.DropColumn(
                name: "action",
                schema: "accounts",
                table: "permission");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "accounts",
                table: "role_stadium",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "accounts",
                table: "role_stadium",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "accounts",
                table: "role_permission",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "accounts",
                table: "role_permission",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "action",
                schema: "accounts",
                table: "permission",
                type: "text",
                nullable: true);
        }
    }
}
