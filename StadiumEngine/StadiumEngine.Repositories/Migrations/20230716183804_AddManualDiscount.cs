using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddManualDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "discount",
                schema: "bookings",
                table: "booking",
                newName: "promo_discount");

            migrationBuilder.AddColumn<decimal>(
                name: "manual_discount",
                schema: "bookings",
                table: "booking",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "manual_discount",
                schema: "bookings",
                table: "booking");

            migrationBuilder.RenameColumn(
                name: "promo_discount",
                schema: "bookings",
                table: "booking",
                newName: "discount");
        }
    }
}
