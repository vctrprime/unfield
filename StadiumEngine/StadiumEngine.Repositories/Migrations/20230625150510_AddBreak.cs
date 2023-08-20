using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddBreak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "break",
                schema: "settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    starthour = table.Column<int>(name: "start_hour", type: "integer", nullable: false),
                    endhour = table.Column<int>(name: "end_hour", type: "integer", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "boolean", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    datestart = table.Column<DateTime>(name: "date_start", type: "timestamp with time zone", nullable: false),
                    dateend = table.Column<DateTime>(name: "date_end", type: "timestamp with time zone", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_break", x => x.id);
                    table.ForeignKey(
                        name: "FK_break_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_break_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_break_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "break_field",
                schema: "settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    breakid = table.Column<int>(name: "break_id", type: "integer", nullable: false),
                    fieldid = table.Column<int>(name: "field_id", type: "integer", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_break_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_break_field_break_break_id",
                        column: x => x.breakid,
                        principalSchema: "settings",
                        principalTable: "break",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_break_field_field_field_id",
                        column: x => x.fieldid,
                        principalSchema: "offers",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_break_field_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_break_field_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_break_stadium_id",
                schema: "settings",
                table: "break",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_break_user_created_id",
                schema: "settings",
                table: "break",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_break_user_modified_id",
                schema: "settings",
                table: "break",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_break_field_break_id",
                schema: "settings",
                table: "break_field",
                column: "break_id");

            migrationBuilder.CreateIndex(
                name: "IX_break_field_field_id",
                schema: "settings",
                table: "break_field",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_break_field_user_created_id",
                schema: "settings",
                table: "break_field",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_break_field_user_modified_id",
                schema: "settings",
                table: "break_field",
                column: "user_modified_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "break_field",
                schema: "settings");

            migrationBuilder.DropTable(
                name: "break",
                schema: "settings");
        }
    }
}
