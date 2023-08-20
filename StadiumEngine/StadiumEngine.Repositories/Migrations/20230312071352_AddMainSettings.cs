using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddMainSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "settings");

            migrationBuilder.CreateTable(
                name: "stadium_main_settings",
                schema: "settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    opentime = table.Column<string>(name: "open_time", type: "text", nullable: true),
                    closetime = table.Column<string>(name: "close_time", type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true),
                    UserCreatedId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stadium_main_settings", x => x.id);
                    table.ForeignKey(
                        name: "FK_stadium_main_settings_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stadium_main_settings_user_UserCreatedId1",
                        column: x => x.UserCreatedId1,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_stadium_main_settings_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_stadium_main_settings_stadium_id",
                schema: "settings",
                table: "stadium_main_settings",
                column: "stadium_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_stadium_main_settings_user_modified_id",
                schema: "settings",
                table: "stadium_main_settings",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_stadium_main_settings_UserCreatedId1",
                schema: "settings",
                table: "stadium_main_settings",
                column: "UserCreatedId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stadium_main_settings",
                schema: "settings");
        }
    }
}
