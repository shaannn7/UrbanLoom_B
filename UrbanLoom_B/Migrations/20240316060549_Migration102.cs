using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanLoom_B.Migrations
{
    /// <inheritdoc />
    public partial class Migration102 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users_ul",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "User",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users_ul",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "User");
        }
    }
}
