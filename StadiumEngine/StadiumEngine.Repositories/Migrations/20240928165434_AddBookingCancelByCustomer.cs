using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingCancelByCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "exclude_by_customer",
                schema: "bookings",
                table: "booking_weekly_exclude_day",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "cancel_by_customer",
                schema: "bookings",
                table: "booking",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "exclude_by_customer",
                schema: "bookings",
                table: "booking_weekly_exclude_day");

            migrationBuilder.DropColumn(
                name: "cancel_by_customer",
                schema: "bookings",
                table: "booking");
        }
    }
}
