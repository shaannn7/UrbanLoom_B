﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanLoom_B.Migrations
{
    /// <inheritdoc />
    public partial class Migration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users_ul",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users_ul",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "user",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Admin");
        }
    }
}
