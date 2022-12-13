using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StadiumEngine.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class datesrem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "geo",
                table: "region");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "geo",
                table: "region");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "accounts",
                table: "legal");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "accounts",
                table: "legal");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "geo",
                table: "country");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "geo",
                table: "country");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "geo",
                table: "city");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "geo",
                table: "city");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "geo",
                table: "region",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "geo",
                table: "region",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "accounts",
                table: "legal",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "accounts",
                table: "legal",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "geo",
                table: "country",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "geo",
                table: "country",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "geo",
                table: "city",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "geo",
                table: "city",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
