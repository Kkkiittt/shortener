using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PlanCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Plans",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SubscriptionPeriod",
                table: "Plans",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Cost", "SubscriptionPeriod" },
                values: new object[] { 0.0, new TimeSpan(9223372036854775807) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "SubscriptionPeriod",
                table: "Plans");
        }
    }
}
