using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class RenameDayIntervalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "start_id",
                schema: "rates",
                table: "day_interval",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "end_id",
                schema: "rates",
                table: "day_interval",
                newName: "end");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "start",
                schema: "rates",
                table: "day_interval",
                newName: "start_id");

            migrationBuilder.RenameColumn(
                name: "end",
                schema: "rates",
                table: "day_interval",
                newName: "end_id");
        }
    }
}
