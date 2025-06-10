using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AdminPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "Actions", "Cost", "Created", "Description", "MaxLinkCount", "MaxLinkLifetime", "Name", "SubscriptionPeriod", "Updated" },
                values: new object[] { 2L, new[] { 1, 3, 4, 2 }, 1.7976931348623157E+308, new DateTime(2022, 5, 12, 7, 12, 12, 123, DateTimeKind.Utc), "Admin tier", 2147483647, 2147483647, "Admin", new TimeSpan(9223372036854775807), new DateTime(2022, 5, 12, 7, 12, 12, 123, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
