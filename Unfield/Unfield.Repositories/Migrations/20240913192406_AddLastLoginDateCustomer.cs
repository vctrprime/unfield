using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unfield.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddLastLoginDateCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_date",
                schema: "customers",
                table: "customer",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_login_date",
                schema: "customers",
                table: "customer");
        }
    }
}
