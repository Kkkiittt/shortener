using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authenticator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "Owner");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "Admin");
        }
    }
}
