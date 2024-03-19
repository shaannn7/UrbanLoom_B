using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanLoom_B.Migrations
{
    /// <inheritdoc />
    public partial class Migration103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderString",
                table: "Orders_ul",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Orders_ul",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderString",
                table: "Orders_ul");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Orders_ul");
        }
    }
}
