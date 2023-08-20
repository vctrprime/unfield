using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "field",
                schema: "offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parentfieldid = table.Column<int>(name: "parent_field_id", type: "integer", nullable: true),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false),
                    coveringtype = table.Column<byte>(name: "covering_type", type: "smallint", nullable: false),
                    width = table.Column<decimal>(type: "numeric", nullable: false),
                    length = table.Column<decimal>(type: "numeric", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_field_field_parent_field_id",
                        column: x => x.parentfieldid,
                        principalSchema: "offers",
                        principalTable: "field",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_field_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_field_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_field_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "field_sport",
                schema: "offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fieldid = table.Column<int>(name: "field_id", type: "integer", nullable: false),
                    sportkind = table.Column<byte>(name: "sport_kind", type: "smallint", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_sport", x => x.id);
                    table.ForeignKey(
                        name: "FK_field_sport_field_field_id",
                        column: x => x.fieldid,
                        principalSchema: "offers",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_field_sport_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_field_sport_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "image",
                schema: "offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    path = table.Column<string>(type: "text", nullable: false),
                    ordervalue = table.Column<int>(name: "order_value", type: "integer", nullable: false),
                    fieldid = table.Column<int>(name: "field_id", type: "integer", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_image_field_field_id",
                        column: x => x.fieldid,
                        principalSchema: "offers",
                        principalTable: "field",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_image_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_image_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_field_parent_field_id",
                schema: "offers",
                table: "field",
                column: "parent_field_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_stadium_id",
                schema: "offers",
                table: "field",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_user_created_id",
                schema: "offers",
                table: "field",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_user_modified_id",
                schema: "offers",
                table: "field",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_sport_field_id",
                schema: "offers",
                table: "field_sport",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_sport_user_created_id",
                schema: "offers",
                table: "field_sport",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_sport_user_modified_id",
                schema: "offers",
                table: "field_sport",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_field_id",
                schema: "offers",
                table: "image",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_user_created_id",
                schema: "offers",
                table: "image",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_user_modified_id",
                schema: "offers",
                table: "image",
                column: "user_modified_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "field_sport",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "image",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "field",
                schema: "offers");
        }
    }
}
