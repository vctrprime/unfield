using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StadiumEngine.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class migr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "geo");

            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.CreateTable(
                name: "country",
                schema: "geo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    shortname = table.Column<string>(name: "short_name", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "region",
                schema: "geo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    countryid = table.Column<int>(name: "country_id", type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    shortname = table.Column<string>(name: "short_name", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region", x => x.id);
                    table.ForeignKey(
                        name: "FK_region_country_country_id",
                        column: x => x.countryid,
                        principalSchema: "geo",
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "city",
                schema: "geo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    regionid = table.Column<int>(name: "region_id", type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    shortname = table.Column<string>(name: "short_name", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.id);
                    table.ForeignKey(
                        name: "FK_city_region_region_id",
                        column: x => x.regionid,
                        principalSchema: "geo",
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "legal",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inn = table.Column<string>(type: "text", nullable: true),
                    headname = table.Column<string>(name: "head_name", type: "text", nullable: true),
                    cityid = table.Column<int>(name: "city_id", type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_legal", x => x.id);
                    table.ForeignKey(
                        name: "FK_legal_city_city_id",
                        column: x => x.cityid,
                        principalSchema: "geo",
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_city_region_id",
                schema: "geo",
                table: "city",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_legal_city_id",
                schema: "accounts",
                table: "legal",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_region_country_id",
                schema: "geo",
                table: "region",
                column: "country_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "legal",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "city",
                schema: "geo");

            migrationBuilder.DropTable(
                name: "region",
                schema: "geo");

            migrationBuilder.DropTable(
                name: "country",
                schema: "geo");
        }
    }
}
