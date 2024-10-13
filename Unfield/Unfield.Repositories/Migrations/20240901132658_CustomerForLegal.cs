using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class CustomerForLegal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_stadium_stadium_id",
                schema: "customers",
                table: "customer");

            migrationBuilder.RenameColumn(
                name: "stadium_id",
                schema: "customers",
                table: "customer",
                newName: "stadium_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_customer_stadium_id_phone_number",
                schema: "customers",
                table: "customer",
                newName: "IX_customer_stadium_group_id_phone_number");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_legal_stadium_group_id",
                schema: "customers",
                table: "customer",
                column: "stadium_group_id",
                principalSchema: "accounts",
                principalTable: "legal",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_legal_stadium_group_id",
                schema: "customers",
                table: "customer");

            migrationBuilder.RenameColumn(
                name: "stadium_group_id",
                schema: "customers",
                table: "customer",
                newName: "stadium_id");

            migrationBuilder.RenameIndex(
                name: "IX_customer_stadium_group_id_phone_number",
                schema: "customers",
                table: "customer",
                newName: "IX_customer_stadium_id_phone_number");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_stadium_stadium_id",
                schema: "customers",
                table: "customer",
                column: "stadium_id",
                principalSchema: "accounts",
                principalTable: "stadium",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
