using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BasePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "Actions", "Created", "Description", "MaxLinkCount", "MaxLinkLifetime", "Name", "Updated" },
                values: new object[] { 1L, new[] { 1, 3, 4 }, new DateTime(2022, 5, 12, 7, 12, 12, 123, DateTimeKind.Utc), "Free tier", 3, 7, "Free", new DateTime(2022, 5, 12, 7, 12, 12, 123, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
