using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanLoom_B.Migrations
{
    /// <inheritdoc />
    public partial class UrbanLoomMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isBlocked",
                table: "Users_ul",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users_ul",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "user",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders_ul",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Processing",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isBlocked",
                table: "Users_ul",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users_ul",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "user");

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders_ul",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Processing");
        }
    }
}
