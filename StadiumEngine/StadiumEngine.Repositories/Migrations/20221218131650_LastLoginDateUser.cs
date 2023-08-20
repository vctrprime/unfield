using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class LastLoginDateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_date",
                schema: "accounts",
                table: "user",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_login_date",
                schema: "accounts",
                table: "user");
        }
    }
}
