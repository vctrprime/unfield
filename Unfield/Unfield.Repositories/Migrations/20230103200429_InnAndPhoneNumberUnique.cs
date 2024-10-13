using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InnAndPhoneNumberUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_user_phone_number",
                schema: "accounts",
                table: "user",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_legal_inn",
                schema: "accounts",
                table: "legal",
                column: "inn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_phone_number",
                schema: "accounts",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_legal_inn",
                schema: "accounts",
                table: "legal");
        }
    }
}
