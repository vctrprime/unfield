using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AlterRatesEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_price_rate_day_interval_rate_day_interval_id",
                schema: "rates",
                table: "price");

            migrationBuilder.DropTable(
                name: "rate_day_interval",
                schema: "rates");

            migrationBuilder.DropTable(
                name: "rate",
                schema: "rates");

            migrationBuilder.RenameColumn(
                name: "rate_day_interval_id",
                schema: "rates",
                table: "price",
                newName: "tariff_day_interval_id");

            migrationBuilder.RenameIndex(
                name: "IX_price_rate_day_interval_id",
                schema: "rates",
                table: "price",
                newName: "IX_price_tariff_day_interval_id");

            migrationBuilder.CreateTable(
                name: "tariff",
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
                    table.PrimaryKey("PK_tariff", x => x.id);
                    table.ForeignKey(
                        name: "FK_tariff_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tariff_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tariff_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tariff_day_interval",
                schema: "rates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tariffid = table.Column<int>(name: "tariff_id", type: "integer", nullable: false),
                    dayintervalid = table.Column<int>(name: "day_interval_id", type: "integer", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tariff_day_interval", x => x.id);
                    table.ForeignKey(
                        name: "FK_tariff_day_interval_day_interval_day_interval_id",
                        column: x => x.dayintervalid,
                        principalSchema: "rates",
                        principalTable: "day_interval",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tariff_day_interval_tariff_tariff_id",
                        column: x => x.tariffid,
                        principalSchema: "rates",
                        principalTable: "tariff",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tariff_day_interval_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tariff_day_interval_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tariff_stadium_id",
                schema: "rates",
                table: "tariff",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_tariff_user_created_id",
                schema: "rates",
                table: "tariff",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_tariff_user_modified_id",
                schema: "rates",
                table: "tariff",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_tariff_day_interval_day_interval_id",
                schema: "rates",
                table: "tariff_day_interval",
                column: "day_interval_id");

            migrationBuilder.CreateIndex(
                name: "IX_tariff_day_interval_tariff_id",
                schema: "rates",
                table: "tariff_day_interval",
                column: "tariff_id");

            migrationBuilder.CreateIndex(
                name: "IX_tariff_day_interval_user_created_id",
                schema: "rates",
                table: "tariff_day_interval",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_tariff_day_interval_user_modified_id",
                schema: "rates",
                table: "tariff_day_interval",
                column: "user_modified_id");

            migrationBuilder.AddForeignKey(
                name: "FK_price_tariff_day_interval_tariff_day_interval_id",
                schema: "rates",
                table: "price",
                column: "tariff_day_interval_id",
                principalSchema: "rates",
                principalTable: "tariff_day_interval",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_price_tariff_day_interval_tariff_day_interval_id",
                schema: "rates",
                table: "price");

            migrationBuilder.DropTable(
                name: "tariff_day_interval",
                schema: "rates");

            migrationBuilder.DropTable(
                name: "tariff",
                schema: "rates");

            migrationBuilder.RenameColumn(
                name: "tariff_day_interval_id",
                schema: "rates",
                table: "price",
                newName: "rate_day_interval_id");

            migrationBuilder.RenameIndex(
                name: "IX_price_tariff_day_interval_id",
                schema: "rates",
                table: "price",
                newName: "IX_price_rate_day_interval_id");

            migrationBuilder.CreateTable(
                name: "rate",
                schema: "rates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    dateend = table.Column<DateTime>(name: "date_end", type: "timestamp with time zone", nullable: true),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    datestart = table.Column<DateTime>(name: "date_start", type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    friday = table.Column<bool>(type: "boolean", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "boolean", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    monday = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    saturday = table.Column<bool>(type: "boolean", nullable: false),
                    sunday = table.Column<bool>(type: "boolean", nullable: false),
                    thursday = table.Column<bool>(type: "boolean", nullable: false),
                    tuesday = table.Column<bool>(type: "boolean", nullable: false),
                    wednesday = table.Column<bool>(type: "boolean", nullable: false)
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
                    dayintervalid = table.Column<int>(name: "day_interval_id", type: "integer", nullable: false),
                    rateid = table.Column<int>(name: "rate_id", type: "integer", nullable: false),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true)
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
                name: "FK_price_rate_day_interval_rate_day_interval_id",
                schema: "rates",
                table: "price",
                column: "rate_day_interval_id",
                principalSchema: "rates",
                principalTable: "rate_day_interval",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
