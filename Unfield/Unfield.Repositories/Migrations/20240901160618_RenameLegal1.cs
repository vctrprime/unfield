using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class RenameLegal1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inn",
                schema: "accounts",
                table: "stadium_group");
            
            migrationBuilder.DropColumn(
                name: "head_name",
                schema: "accounts",
                table: "stadium_group");
            
            migrationBuilder.DropColumn(
                name: "city_id",
                schema: "accounts",
                table: "stadium_group");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
