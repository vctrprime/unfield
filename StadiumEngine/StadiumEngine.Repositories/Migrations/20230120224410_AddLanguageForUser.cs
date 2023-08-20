using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "language",
                schema: "accounts",
                table: "user",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "language",
                schema: "accounts",
                table: "user");
        }
    }
}
