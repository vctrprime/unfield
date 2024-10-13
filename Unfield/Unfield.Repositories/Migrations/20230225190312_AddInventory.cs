using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "field_sport",
                schema: "offers");

            migrationBuilder.AddColumn<int>(
                name: "inventory_id",
                schema: "offers",
                table: "image",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "inventory",
                schema: "offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currency = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true),
                    stadiumid = table.Column<int>(name: "stadium_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.id);
                    table.ForeignKey(
                        name: "FK_inventory_stadium_stadium_id",
                        column: x => x.stadiumid,
                        principalSchema: "accounts",
                        principalTable: "stadium",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_inventory_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sport_kind",
                schema: "offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fieldid = table.Column<int>(name: "field_id", type: "integer", nullable: true),
                    inventoryid = table.Column<int>(name: "inventory_id", type: "integer", nullable: true),
                    sportkind = table.Column<byte>(name: "sport_kind", type: "smallint", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sport_kind", x => x.id);
                    table.ForeignKey(
                        name: "FK_sport_kind_field_field_id",
                        column: x => x.fieldid,
                        principalSchema: "offers",
                        principalTable: "field",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_sport_kind_inventory_inventory_id",
                        column: x => x.inventoryid,
                        principalSchema: "offers",
                        principalTable: "inventory",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_sport_kind_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_sport_kind_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_image_inventory_id",
                schema: "offers",
                table: "image",
                column: "inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_stadium_id",
                schema: "offers",
                table: "inventory",
                column: "stadium_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_user_created_id",
                schema: "offers",
                table: "inventory",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_user_modified_id",
                schema: "offers",
                table: "inventory",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_sport_kind_field_id",
                schema: "offers",
                table: "sport_kind",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_sport_kind_inventory_id",
                schema: "offers",
                table: "sport_kind",
                column: "inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_sport_kind_user_created_id",
                schema: "offers",
                table: "sport_kind",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_sport_kind_user_modified_id",
                schema: "offers",
                table: "sport_kind",
                column: "user_modified_id");

            migrationBuilder.AddForeignKey(
                name: "FK_image_inventory_inventory_id",
                schema: "offers",
                table: "image",
                column: "inventory_id",
                principalSchema: "offers",
                principalTable: "inventory",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_inventory_inventory_id",
                schema: "offers",
                table: "image");

            migrationBuilder.DropTable(
                name: "sport_kind",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "inventory",
                schema: "offers");

            migrationBuilder.DropIndex(
                name: "IX_image_inventory_id",
                schema: "offers",
                table: "image");

            migrationBuilder.DropColumn(
                name: "inventory_id",
                schema: "offers",
                table: "image");

            migrationBuilder.CreateTable(
                name: "field_sport",
                schema: "offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fieldid = table.Column<int>(name: "field_id", type: "integer", nullable: false),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    sportkind = table.Column<byte>(name: "sport_kind", type: "smallint", nullable: false)
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
        }
    }
}
