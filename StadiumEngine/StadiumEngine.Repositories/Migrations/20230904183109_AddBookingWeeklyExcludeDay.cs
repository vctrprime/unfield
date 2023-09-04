using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingWeeklyExcludeDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "booking_weekly_exclude_day",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bookingid = table.Column<int>(name: "booking_id", type: "integer", nullable: false),
                    day = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_weekly_exclude_day", x => x.id);
                    table.ForeignKey(
                        name: "FK_booking_weekly_exclude_day_booking_booking_id",
                        column: x => x.bookingid,
                        principalSchema: "bookings",
                        principalTable: "booking",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_weekly_exclude_day_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_booking_weekly_exclude_day_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_weekly_exclude_day_booking_id",
                schema: "bookings",
                table: "booking_weekly_exclude_day",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_weekly_exclude_day_user_created_id",
                schema: "bookings",
                table: "booking_weekly_exclude_day",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_weekly_exclude_day_user_modified_id",
                schema: "bookings",
                table: "booking_weekly_exclude_day",
                column: "user_modified_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_weekly_exclude_day",
                schema: "bookings");
        }
    }
}
