using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddPromoCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "promo_code",
                schema: "rates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<byte>(type: "smallint", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    tariffid = table.Column<int>(name: "tariff_id", type: "integer", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    datemodified = table.Column<DateTime>(name: "date_modified", type: "timestamp with time zone", nullable: true),
                    usercreatedid = table.Column<int>(name: "user_created_id", type: "integer", nullable: true),
                    usermodifiedid = table.Column<int>(name: "user_modified_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_code", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_code_tariff_tariff_id",
                        column: x => x.tariffid,
                        principalSchema: "rates",
                        principalTable: "tariff",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_promo_code_user_user_created_id",
                        column: x => x.usercreatedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_promo_code_user_user_modified_id",
                        column: x => x.usermodifiedid,
                        principalSchema: "accounts",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_promo_code_tariff_id",
                schema: "rates",
                table: "promo_code",
                column: "tariff_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_code_user_created_id",
                schema: "rates",
                table: "promo_code",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_code_user_modified_id",
                schema: "rates",
                table: "promo_code",
                column: "user_modified_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "promo_code",
                schema: "rates");
        }
    }
}
