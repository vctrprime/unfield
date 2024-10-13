using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddRatesEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rates");

            migrationBuilder.AddColumn<int>(
                name: "price_group_id",
                schema: "offers",
                table: "field",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "day_interval",
                schema: "rates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    startid = table.Column<string>(name: "start_id", type: "text", nullable: true),
                    endid = table.Column<string>(name: "end_id", type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_day_interval", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PriceGroups",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "boolean", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceGroups", x => x.id);
                    table.ForeignKey(
                        name: "FK_PriceGroups_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceGroups_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PriceGroups_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rate",
                schema: "rates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datestart = table.Column<DateTime>(name: "date_start", type: "timestamp with time zone", nullable: false),
                    dateend = table.Column<DateTime>(name: "date_end", type: "timestamp with time zone", nullable: true),
                    monday = table.Column<bool>(type: "boolean", nullable: false),
                    tuesday = table.Column<bool>(type: "boolean", nullable: false),
                    wednesday = table.Column<bool>(type: "boolean", nullable: false),
                    thursday = table.Column<bool>(type: "boolean", nullable: false),
                    friday = table.Column<bool>(type: "boolean", nullable: false),
                    saturday = table.Column<bool>(type: "boolean", nullable: false),
                    sunday = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "boolean", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rate", x => x.id);
                    table.ForeignKey(
                        name: "FK_rate_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rate_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_rate_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rate_day_interval",
                schema: "rates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rateid = table.Column<int>(name: "rate_id", type: "integer", nullable: false),
                    dayintervalid = table.Column<int>(name: "day_interval_id", type: "integer", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rate_day_interval", x => x.id);
                    table.ForeignKey(
                        name: "FK_rate_day_interval_day_interval_day_interval_id",
                        column: x => x.dayintervalid,
                        principalSchema: "rates",
                        principalTable: "day_interval",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rate_day_interval_rate_rate_id",
                        column: x => x.rateid,
                        principalSchema: "rates",
                        principalTable: "rate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rate_day_interval_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_rate_day_interval_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "price",
                schema: "rates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fieldid = table.Column<int>(name: "field_id", type: "integer", nullable: false),
                    ratedayintervalid = table.Column<int>(name: "rate_day_interval_id", type: "integer", nullable: false),
                    isobsolete = table.Column<bool>(name: "is_obsolete", type: "boolean", nullable: false),
                    currency = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_price", x => x.id);
                    table.ForeignKey(
                        name: "FK_price_field_field_id",
                        column: x => x.fieldid,
                        principalSchema: "offers",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_price_rate_day_interval_rate_day_interval_id",
                        column: x => x.ratedayintervalid,
                        principalSchema: "rates",
                        principalTable: "rate_day_interval",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_price_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_price_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_field_price_group_id",
                schema: "offers",
                table: "field",
                column: "price_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_price_field_id",
                schema: "rates",
                table: "price",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_price_rate_day_interval_id",
                schema: "rates",
                table: "price",
                column: "rate_day_interval_id");

            migrationBuilder.CreateIndex(
                name: "IX_price_user_created_id",
                schema: "rates",
                table: "price",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_price_user_modified_id",
                schema: "rates",
                table: "price",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_PriceGroups_stadium_id",
                table: "PriceGroups",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_PriceGroups_user_created_id",
                table: "PriceGroups",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_PriceGroups_user_modified_id",
                table: "PriceGroups",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_rate_stadium_id",
                schema: "rates",
                table: "rate",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_rate_user_created_id",
                schema: "rates",
                table: "rate",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_rate_user_modified_id",
                schema: "rates",
                table: "rate",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_rate_day_interval_day_interval_id",
                schema: "rates",
                table: "rate_day_interval",
                column: "day_interval_id");

            migrationBuilder.CreateIndex(
                name: "IX_rate_day_interval_rate_id",
                schema: "rates",
                table: "rate_day_interval",
                column: "rate_id");

            migrationBuilder.CreateIndex(
                name: "IX_rate_day_interval_user_created_id",
                schema: "rates",
                table: "rate_day_interval",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_rate_day_interval_user_modified_id",
                schema: "rates",
                table: "rate_day_interval",
                column: "user_modified_id");

            migrationBuilder.AddForeignKey(
                name: "FK_field_PriceGroups_price_group_id",
                schema: "offers",
                table: "field",
                column: "price_group_id",
                principalTable: "PriceGroups",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_field_PriceGroups_price_group_id",
                schema: "offers",
                table: "field");

            migrationBuilder.DropTable(
                name: "price",
                schema: "rates");

            migrationBuilder.DropTable(
                name: "PriceGroups");

            migrationBuilder.DropTable(
                name: "rate_day_interval",
                schema: "rates");

            migrationBuilder.DropTable(
                name: "day_interval",
                schema: "rates");

            migrationBuilder.DropTable(
                name: "rate",
                schema: "rates");

            migrationBuilder.DropIndex(
                name: "IX_field_price_group_id",
                schema: "offers",
                table: "field");

            migrationBuilder.DropColumn(
                name: "price_group_id",
                schema: "offers",
                table: "field");
        }
    }
}
