using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddBookings1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hour",
                schema: "bookings",
                table: "booking_cost");

            migrationBuilder.AddColumn<decimal>(
                name: "end_hour",
                schema: "bookings",
                table: "booking_cost",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "start_hour",
                schema: "bookings",
                table: "booking_cost",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "start_hour",
                schema: "bookings",
                table: "booking",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "hours_count",
                schema: "bookings",
                table: "booking",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_hour",
                schema: "bookings",
                table: "booking_cost");

            migrationBuilder.DropColumn(
                name: "start_hour",
                schema: "bookings",
                table: "booking_cost");

            migrationBuilder.AddColumn<int>(
                name: "hour",
                schema: "bookings",
                table: "booking_cost",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "start_hour",
                schema: "bookings",
                table: "booking",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "hours_count",
                schema: "bookings",
                table: "booking",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
