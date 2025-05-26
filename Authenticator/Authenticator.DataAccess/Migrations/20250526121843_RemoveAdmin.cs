using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authenticator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Created", "Email", "Name", "PasswordHash", "Role", "SubscriptionId", "Updated" },
                values: new object[] { 1L, 1000000000.0, new DateTime(1111, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khamidov357@gmail.com", "Owner", "$2a$11$VDoh9iW0FJQwIgJ2f6Dlveq/GWpULWsPsLa6Po2LapDkpRdlgyI.u", 0, 0L, new DateTime(1111, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
