using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingLockerRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_locker_room_locker_room_id",
                schema: "bookings",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_main_settings_user_UserCreatedId1",
                schema: "settings",
                table: "main_settings");

            migrationBuilder.DropIndex(
                name: "IX_main_settings_UserCreatedId1",
                schema: "settings",
                table: "main_settings");

            migrationBuilder.DropIndex(
                name: "IX_booking_locker_room_id",
                schema: "bookings",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "UserCreatedId1",
                schema: "settings",
                table: "main_settings");

            migrationBuilder.DropColumn(
                name: "locker_room_id",
                schema: "bookings",
                table: "booking");

            migrationBuilder.CreateTable(
                name: "booking_locker_room",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bookingid = table.Column<int>(name: "booking_id", type: "integer", nullable: false),
                    lockerroomid = table.Column<int>(name: "locker_room_id", type: "integer", nullable: false),
                    start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_locker_room", x => x.id);
                    table.ForeignKey(
                        name: "FK_booking_locker_room_booking_booking_id",
                        column: x => x.bookingid,
                        principalSchema: "bookings",
                        principalTable: "booking",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_locker_room_locker_room_locker_room_id",
                        column: x => x.lockerroomid,
                        principalSchema: "offers",
                        principalTable: "locker_room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_locker_room_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_booking_locker_room_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_locker_room_booking_id",
                schema: "bookings",
                table: "booking_locker_room",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_booking_locker_room_locker_room_id",
                schema: "bookings",
                table: "booking_locker_room",
                column: "locker_room_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_locker_room_user_created_id",
                schema: "bookings",
                table: "booking_locker_room",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_locker_room_user_modified_id",
                schema: "bookings",
                table: "booking_locker_room",
                column: "user_modified_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_locker_room",
                schema: "bookings");

            migrationBuilder.AddColumn<int>(
                name: "UserCreatedId1",
                schema: "settings",
                table: "main_settings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "locker_room_id",
                schema: "bookings",
                table: "booking",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_main_settings_UserCreatedId1",
                schema: "settings",
                table: "main_settings",
                column: "UserCreatedId1");

            migrationBuilder.CreateIndex(
                name: "IX_booking_locker_room_id",
                schema: "bookings",
                table: "booking",
                column: "locker_room_id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_locker_room_locker_room_id",
                schema: "bookings",
                table: "booking",
                column: "locker_room_id",
                principalSchema: "offers",
                principalTable: "locker_room",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_main_settings_user_UserCreatedId1",
                schema: "settings",
                table: "main_settings",
                column: "UserCreatedId1",
                principalSchema: "accounts",
                principalTable: "user",
                principalColumn: "id");
        }
    }
}
