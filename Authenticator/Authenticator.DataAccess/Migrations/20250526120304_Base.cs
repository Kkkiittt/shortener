using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authenticator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "$2a$11$VDoh9iW0FJQwIgJ2f6Dlveq/GWpULWsPsLa6Po2LapDkpRdlgyI.u");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "$2a$11$OpqvJVJFUMOuiMnBYQm1rentruELV/8lamMd2jLm7EUHgchRiUOyi");
        }
    }
}
