using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddSplitAmountsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "amount",
                schema: "bookings",
                table: "booking",
                newName: "total_amount_before_discount");

            migrationBuilder.AddColumn<decimal>(
                name: "field_amount",
                schema: "bookings",
                table: "booking",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "inventory_amount",
                schema: "bookings",
                table: "booking",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total_amount_after_discount",
                schema: "bookings",
                table: "booking",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "field_amount",
                schema: "bookings",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "inventory_amount",
                schema: "bookings",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "total_amount_after_discount",
                schema: "bookings",
                table: "booking");

            migrationBuilder.RenameColumn(
                name: "total_amount_before_discount",
                schema: "bookings",
                table: "booking",
                newName: "amount");
        }
    }
}
