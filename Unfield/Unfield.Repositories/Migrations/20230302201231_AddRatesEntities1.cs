using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddRatesEntities1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_field_PriceGroups_price_group_id",
                schema: "offers",
                table: "field");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceGroups_stadium_stadium_id",
                table: "PriceGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceGroups_user_user_created_id",
                table: "PriceGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceGroups_user_user_modified_id",
                table: "PriceGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceGroups",
                table: "PriceGroups");

            migrationBuilder.RenameTable(
                name: "PriceGroups",
                newName: "price_group",
                newSchema: "rates");

            migrationBuilder.RenameIndex(
                name: "IX_PriceGroups_user_modified_id",
                schema: "rates",
                table: "price_group",
                newName: "IX_price_group_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_PriceGroups_user_created_id",
                schema: "rates",
                table: "price_group",
                newName: "IX_price_group_user_created_id");

            migrationBuilder.RenameIndex(
                name: "IX_PriceGroups_stadium_id",
                schema: "rates",
                table: "price_group",
                newName: "IX_price_group_stadium_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_price_group",
                schema: "rates",
                table: "price_group",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_field_price_group_price_group_id",
                schema: "offers",
                table: "field",
                column: "price_group_id",
                principalSchema: "rates",
                principalTable: "price_group",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_price_group_stadium_stadium_id",
                schema: "rates",
                table: "price_group",
                column: "stadium_id",
                principalSchema: "accounts",
                principalTable: "stadium",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_price_group_user_user_created_id",
                schema: "rates",
                table: "price_group",
                column: "user_created_id",
                principalSchema: "accounts",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_price_group_user_user_modified_id",
                schema: "rates",
                table: "price_group",
                column: "user_modified_id",
                principalSchema: "accounts",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_field_price_group_price_group_id",
                schema: "offers",
                table: "field");

            migrationBuilder.DropForeignKey(
                name: "FK_price_group_stadium_stadium_id",
                schema: "rates",
                table: "price_group");

            migrationBuilder.DropForeignKey(
                name: "FK_price_group_user_user_created_id",
                schema: "rates",
                table: "price_group");

            migrationBuilder.DropForeignKey(
                name: "FK_price_group_user_user_modified_id",
                schema: "rates",
                table: "price_group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_price_group",
                schema: "rates",
                table: "price_group");

            migrationBuilder.RenameTable(
                name: "price_group",
                schema: "rates",
                newName: "PriceGroups");

            migrationBuilder.RenameIndex(
                name: "IX_price_group_user_modified_id",
                table: "PriceGroups",
                newName: "IX_PriceGroups_user_modified_id");

            migrationBuilder.RenameIndex(
                name: "IX_price_group_user_created_id",
                table: "PriceGroups",
                newName: "IX_PriceGroups_user_created_id");

            migrationBuilder.RenameIndex(
                name: "IX_price_group_stadium_id",
                table: "PriceGroups",
                newName: "IX_PriceGroups_stadium_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceGroups",
                table: "PriceGroups",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_field_PriceGroups_price_group_id",
                schema: "offers",
                table: "field",
                column: "price_group_id",
                principalTable: "PriceGroups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceGroups_stadium_stadium_id",
                table: "PriceGroups",
                column: "stadium_id",
                principalSchema: "accounts",
                principalTable: "stadium",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceGroups_user_user_created_id",
                table: "PriceGroups",
                column: "user_created_id",
                principalSchema: "accounts",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceGroups_user_user_modified_id",
                table: "PriceGroups",
                column: "user_modified_id",
                principalSchema: "accounts",
                principalTable: "user",
                principalColumn: "id");
        }
    }
}
