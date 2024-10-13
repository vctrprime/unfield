using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bookings");

            migrationBuilder.CreateTable(
                name: "booking",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bookingnumber = table.Column<string>(name: "booking_number", type: "text", nullable: false),
                    source = table.Column<int>(type: "integer", nullable: false),
                    day = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    starthour = table.Column<int>(name: "start_hour", type: "integer", nullable: false),
                    hourscount = table.Column<int>(name: "hours_count", type: "integer", nullable: false),
                    fieldid = table.Column<int>(name: "field_id", type: "integer", nullable: false),
                    tariffid = table.Column<int>(name: "tariff_id", type: "integer", nullable: false),
                    lockerroomid = table.Column<int>(name: "locker_room_id", type: "integer", nullable: true),
                    isdraft = table.Column<bool>(name: "is_draft", type: "boolean", nullable: false),
                    isconfirmed = table.Column<bool>(name: "is_confirmed", type: "boolean", nullable: false),
                    iscanceled = table.Column<bool>(name: "is_canceled", type: "boolean", nullable: false),
                    accesscode = table.Column<string>(name: "access_code", type: "text", nullable: false),
                    promocode = table.Column<string>(name: "promo_code", type: "text", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.id);
                    table.ForeignKey(
                        name: "FK_booking_field_field_id",
                        column: x => x.fieldid,
                        principalSchema: "offers",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_locker_room_locker_room_id",
                        column: x => x.lockerroomid,
                        principalSchema: "offers",
                        principalTable: "locker_room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_booking_tariff_tariff_id",
                        column: x => x.tariffid,
                        principalSchema: "rates",
                        principalTable: "tariff",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_booking_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "booking_cost",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bookingid = table.Column<int>(name: "booking_id", type: "integer", nullable: false),
                    hour = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_cost", x => x.id);
                    table.ForeignKey(
                        name: "FK_booking_cost_booking_booking_id",
                        column: x => x.bookingid,
                        principalSchema: "bookings",
                        principalTable: "booking",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_cost_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_booking_cost_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "booking_customer",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bookingid = table.Column<int>(name: "booking_id", type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    phonenumber = table.Column<string>(name: "phone_number", type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_booking_customer_booking_booking_id",
                        column: x => x.bookingid,
                        principalSchema: "bookings",
                        principalTable: "booking",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_customer_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_booking_customer_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "booking_inventory",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bookingid = table.Column<int>(name: "booking_id", type: "integer", nullable: false),
                    inventoryid = table.Column<int>(name: "inventory_id", type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_inventory", x => x.id);
                    table.ForeignKey(
                        name: "FK_booking_inventory_booking_booking_id",
                        column: x => x.bookingid,
                        principalSchema: "bookings",
                        principalTable: "booking",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_inventory_inventory_inventory_id",
                        column: x => x.inventoryid,
                        principalSchema: "offers",
                        principalTable: "inventory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_inventory_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_booking_inventory_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_field_id",
                schema: "bookings",
                table: "booking",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_locker_room_id",
                schema: "bookings",
                table: "booking",
                column: "locker_room_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_tariff_id",
                schema: "bookings",
                table: "booking",
                column: "tariff_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_user_created_id",
                schema: "bookings",
                table: "booking",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_user_modified_id",
                schema: "bookings",
                table: "booking",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_cost_booking_id",
                schema: "bookings",
                table: "booking_cost",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_cost_user_created_id",
                schema: "bookings",
                table: "booking_cost",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_cost_user_modified_id",
                schema: "bookings",
                table: "booking_cost",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_customer_booking_id",
                schema: "bookings",
                table: "booking_customer",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_booking_customer_user_created_id",
                schema: "bookings",
                table: "booking_customer",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_customer_user_modified_id",
                schema: "bookings",
                table: "booking_customer",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_inventory_booking_id",
                schema: "bookings",
                table: "booking_inventory",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_inventory_inventory_id",
                schema: "bookings",
                table: "booking_inventory",
                column: "inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_inventory_user_created_id",
                schema: "bookings",
                table: "booking_inventory",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_inventory_user_modified_id",
                schema: "bookings",
                table: "booking_inventory",
                column: "user_modified_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_cost",
                schema: "bookings");

            migrationBuilder.DropTable(
                name: "booking_customer",
                schema: "bookings");

            migrationBuilder.DropTable(
                name: "booking_inventory",
                schema: "bookings");

            migrationBuilder.DropTable(
                name: "booking",
                schema: "bookings");
        }
    }
}
