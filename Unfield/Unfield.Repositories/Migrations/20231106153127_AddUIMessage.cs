using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddUIMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "notifications");

            migrationBuilder.CreateTable(
                name: "ui_message",
                schema: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    messagetype = table.Column<int>(name: "message_type", type: "integer", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ui_message", x => x.id);
                    table.ForeignKey(
                        name: "FK_ui_message_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ui_message_last_read",
                schema: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    uimessageid = table.Column<int>(name: "ui_message_id", type: "integer", nullable: false),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ui_message_last_read", x => x.id);
                    table.ForeignKey(
                        name: "FK_ui_message_last_read_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ui_message_last_read_ui_message_ui_message_id",
                        column: x => x.uimessageid,
                        principalSchema: "notifications",
                        principalTable: "ui_message",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ui_message_last_read_user_user_id",
                        column: x => x.userid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ui_message_text",
                schema: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uimessageid = table.Column<int>(name: "ui_message_id", type: "integer", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ui_message_text", x => x.id);
                    table.ForeignKey(
                        name: "FK_ui_message_text_ui_message_ui_message_id",
                        column: x => x.uimessageid,
                        principalSchema: "notifications",
                        principalTable: "ui_message",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ui_message_stadium_id",
                schema: "notifications",
                table: "ui_message",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_ui_message_last_read_stadium_id",
                schema: "notifications",
                table: "ui_message_last_read",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_ui_message_last_read_ui_message_id",
                schema: "notifications",
                table: "ui_message_last_read",
                column: "ui_message_id");

            migrationBuilder.CreateIndex(
                name: "IX_ui_message_last_read_user_id",
                schema: "notifications",
                table: "ui_message_last_read",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ui_message_text_ui_message_id",
                schema: "notifications",
                table: "ui_message_text",
                column: "ui_message_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ui_message_last_read",
                schema: "notifications");

            migrationBuilder.DropTable(
                name: "ui_message_text",
                schema: "notifications");

            migrationBuilder.DropTable(
                name: "ui_message",
                schema: "notifications");
        }
    }
}
