using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddStadiumToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "accounts",
                table: "stadium",
                newName: "address");

            migrationBuilder.AddColumn<string>(
                name: "token",
                schema: "accounts",
                table: "stadium",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                schema: "accounts",
                table: "stadium");

            migrationBuilder.RenameColumn(
                name: "address",
                schema: "accounts",
                table: "stadium",
                newName: "Address");
        }
    }
}
