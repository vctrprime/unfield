using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AlterMainSettings1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "close_time",
                schema: "settings",
                table: "stadium_main_settings");

            migrationBuilder.DropColumn(
                name: "open_time",
                schema: "settings",
                table: "stadium_main_settings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "close_time",
                schema: "settings",
                table: "stadium_main_settings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "open_time",
                schema: "settings",
                table: "stadium_main_settings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
