using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class RenameLegal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_legal_stadium_group_id",
                schema: "customers",
                table: "customer");

            migrationBuilder.DropForeignKey(
                name: "FK_role_legal_legal_id",
                schema: "accounts",
                table: "role");

            migrationBuilder.DropForeignKey(
                name: "FK_stadium_legal_legal_id",
                schema: "accounts",
                table: "stadium");

            migrationBuilder.DropForeignKey(
                name: "FK_user_legal_legal_id",
                schema: "accounts",
                table: "user");
            
            migrationBuilder.RenameColumn(
                name: "legal_id",
                schema: "accounts",
                table: "user",
                newName: "stadium_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_legal_id",
                schema: "accounts",
                table: "user",
                newName: "IX_user_stadium_group_id");

            migrationBuilder.RenameColumn(
                name: "legal_id",
                schema: "accounts",
                table: "stadium",
                newName: "stadium_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_stadium_legal_id",
                schema: "accounts",
                table: "stadium",
                newName: "IX_stadium_stadium_group_id");

            migrationBuilder.RenameColumn(
                name: "legal_id",
                schema: "accounts",
                table: "role",
                newName: "stadium_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_legal_id",
                schema: "accounts",
                table: "role",
                newName: "IX_role_stadium_group_id");

            migrationBuilder.RenameTable( schema: "accounts", name: "legal", newName: "stadium_group" );
            
            migrationBuilder.AddForeignKey(
                name: "FK_customer_stadium_group_stadium_group_id",
                schema: "customers",
                table: "customer",
                column: "stadium_group_id",
                principalSchema: "accounts",
                principalTable: "stadium_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_stadium_group_stadium_group_id",
                schema: "accounts",
                table: "role",
                column: "stadium_group_id",
                principalSchema: "accounts",
                principalTable: "stadium_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stadium_stadium_group_stadium_group_id",
                schema: "accounts",
                table: "stadium",
                column: "stadium_group_id",
                principalSchema: "accounts",
                principalTable: "stadium_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_stadium_group_stadium_group_id",
                schema: "accounts",
                table: "user",
                column: "stadium_group_id",
                principalSchema: "accounts",
                principalTable: "stadium_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_stadium_group_stadium_group_id",
                schema: "customers",
                table: "customer");

            migrationBuilder.DropForeignKey(
                name: "FK_role_stadium_group_stadium_group_id",
                schema: "accounts",
                table: "role");

            migrationBuilder.DropForeignKey(
                name: "FK_stadium_stadium_group_stadium_group_id",
                schema: "accounts",
                table: "stadium");

            migrationBuilder.DropForeignKey(
                name: "FK_user_stadium_group_stadium_group_id",
                schema: "accounts",
                table: "user");

            migrationBuilder.RenameTable( schema: "accounts", name: "stadium_group", newName: "legal" );

            migrationBuilder.RenameColumn(
                name: "stadium_group_id",
                schema: "accounts",
                table: "user",
                newName: "legal_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_stadium_group_id",
                schema: "accounts",
                table: "user",
                newName: "IX_user_legal_id");

            migrationBuilder.RenameColumn(
                name: "stadium_group_id",
                schema: "accounts",
                table: "stadium",
                newName: "legal_id");

            migrationBuilder.RenameIndex(
                name: "IX_stadium_stadium_group_id",
                schema: "accounts",
                table: "stadium",
                newName: "IX_stadium_legal_id");

            migrationBuilder.RenameColumn(
                name: "stadium_group_id",
                schema: "accounts",
                table: "role",
                newName: "legal_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_stadium_group_id",
                schema: "accounts",
                table: "role",
                newName: "IX_role_legal_id");
            
            migrationBuilder.CreateIndex(
                name: "IX_legal_city_id",
                schema: "accounts",
                table: "legal",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_legal_inn",
                schema: "accounts",
                table: "legal",
                column: "inn",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_customer_legal_stadium_group_id",
                schema: "customers",
                table: "customer",
                column: "stadium_group_id",
                principalSchema: "accounts",
                principalTable: "legal",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_legal_legal_id",
                schema: "accounts",
                table: "role",
                column: "legal_id",
                principalSchema: "accounts",
                principalTable: "legal",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stadium_legal_legal_id",
                schema: "accounts",
                table: "stadium",
                column: "legal_id",
                principalSchema: "accounts",
                principalTable: "legal",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_legal_legal_id",
                schema: "accounts",
                table: "user",
                column: "legal_id",
                principalSchema: "accounts",
                principalTable: "legal",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
