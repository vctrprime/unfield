using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStadiumBinging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_stadium",
                schema: "accounts");

            migrationBuilder.CreateTable(
                name: "user_stadium",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_stadium", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_stadium_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_stadium_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_stadium_user_user_id",
                        column: x => x.userid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_stadium_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_stadium_stadium_id",
                schema: "accounts",
                table: "user_stadium",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_stadium_user_created_id",
                schema: "accounts",
                table: "user_stadium",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_stadium_user_id",
                schema: "accounts",
                table: "user_stadium",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_stadium_user_modified_id",
                schema: "accounts",
                table: "user_stadium",
                column: "user_modified_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_stadium",
                schema: "accounts");

            migrationBuilder.CreateTable(
                name: "role_stadium",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_stadium", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_stadium_role_role_id",
                        column: x => x.roleid,
                        principalSchema: "accounts",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_stadium_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_stadium_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_role_stadium_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_role_stadium_role_id",
                schema: "accounts",
                table: "role_stadium",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_stadium_stadium_id",
                schema: "accounts",
                table: "role_stadium",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_stadium_user_created_id",
                schema: "accounts",
                table: "role_stadium",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_stadium_user_modified_id",
                schema: "accounts",
                table: "role_stadium",
                column: "user_modified_id");
        }
    }
}
