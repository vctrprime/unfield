﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AlterMainSettings2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "close_time",
                schema: "settings",
                table: "stadium_main_settings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "open_time",
                schema: "settings",
                table: "stadium_main_settings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "close_time",
                schema: "settings",
                table: "stadium_main_settings");

            migrationBuilder.DropColumn(
                name: "open_time",
                schema: "settings",
                table: "stadium_main_settings");
        }
    }
}
