using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyInBaseUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_user_user_created_id",
                schema: "accounts",
                table: "user",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_user_modified_id",
                schema: "accounts",
                table: "user",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_user_created_id",
                schema: "accounts",
                table: "role",
                column: "user_created_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_user_modified_id",
                schema: "accounts",
                table: "role",
                column: "user_modified_id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_user_user_created_id",
                schema: "accounts",
                table: "role",
                column: "user_created_id",
                principalSchema: "accounts",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_user_user_modified_id",
                schema: "accounts",
                table: "role",
                column: "user_modified_id",
                principalSchema: "accounts",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_user_user_created_id",
                schema: "accounts",
                table: "user",
                column: "user_created_id",
                principalSchema: "accounts",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_user_user_modified_id",
                schema: "accounts",
                table: "user",
                column: "user_modified_id",
                principalSchema: "accounts",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_user_user_created_id",
                schema: "accounts",
                table: "role");

            migrationBuilder.DropForeignKey(
                name: "FK_role_user_user_modified_id",
                schema: "accounts",
                table: "role");

            migrationBuilder.DropForeignKey(
                name: "FK_user_user_user_created_id",
                schema: "accounts",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_user_user_modified_id",
                schema: "accounts",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_user_created_id",
                schema: "accounts",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_user_modified_id",
                schema: "accounts",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_role_user_created_id",
                schema: "accounts",
                table: "role");

            migrationBuilder.DropIndex(
                name: "IX_role_user_modified_id",
                schema: "accounts",
                table: "role");
        }
    }
}
