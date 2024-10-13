using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddStadiumId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stadium_id",
                schema: "customers",
                table: "customer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_customer_stadium_id_phone_number",
                schema: "customers",
                table: "customer",
                columns: new[] { "stadium_id", "phone_number" },
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_stadium_stadium_id",
                schema: "customers",
                table: "customer");

            migrationBuilder.DropIndex(
                name: "IX_customer_stadium_id_phone_number",
                schema: "customers",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "stadium_id",
                schema: "customers",
                table: "customer");
        }
    }
}
