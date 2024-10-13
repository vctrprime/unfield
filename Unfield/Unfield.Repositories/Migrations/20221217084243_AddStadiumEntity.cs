using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Unfield.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStadiumEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "offers");

            migrationBuilder.CreateTable(
                name: "permission",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    permissiongroupid = table.Column<int>(name: "permission_group_id", type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.id);
                    table.ForeignKey(
                        name: "FK_permission_permission_group_permission_group_id",
                        column: x => x.permissiongroupid,
                        principalSchema: "accounts",
                        principalTable: "permission_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stadium",
                schema: "offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cityid = table.Column<int>(name: "city_id", type: "integer", nullable: false),
                    legalid = table.Column<int>(name: "legal_id", type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stadium", x => x.id);
                    table.ForeignKey(
                        name: "FK_stadium_city_city_id",
                        column: x => x.cityid,
                        principalSchema: "geo",
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stadium_legal_legal_id",
                        column: x => x.legalid,
                        principalSchema: "accounts",
                        principalTable: "legal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false),
                    permissionid = table.Column<int>(name: "permission_id", type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_permission_permission_permission_id",
                        column: x => x.permissionid,
                        principalSchema: "accounts",
                        principalTable: "permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_role_role_id",
                        column: x => x.roleid,
                        principalSchema: "accounts",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_role_permission_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_stadium",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
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
                        principalSchema: "offers",
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
                name: "IX_permission_permission_group_id",
                schema: "accounts",
                table: "permission",
                column: "permission_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_permission_id",
                schema: "accounts",
                table: "role_permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_role_id",
                schema: "accounts",
                table: "role_permission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_user_created_id",
                schema: "accounts",
                table: "role_permission",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_user_modified_id",
                schema: "accounts",
                table: "role_permission",
                column: "user_modified_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_stadium_city_id",
                schema: "offers",
                table: "stadium",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_stadium_legal_id",
                schema: "offers",
                table: "stadium",
                column: "legal_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_permission",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "role_stadium",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "permission",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "stadium",
                schema: "offers");
        }
    }
}
