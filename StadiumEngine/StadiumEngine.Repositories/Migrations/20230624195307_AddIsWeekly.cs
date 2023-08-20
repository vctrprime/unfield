using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddIsWeekly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_weekly",
                schema: "bookings",
                table: "booking",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "is_weekly_stopped_date",
                schema: "bookings",
                table: "booking",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_weekly",
                schema: "bookings",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "is_weekly_stopped_date",
                schema: "bookings",
                table: "booking");
        }
    }
}
