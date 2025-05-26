using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Authenticator.DataAccess.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Email = table.Column<string>(type: "text", nullable: false),
					PasswordHash = table.Column<string>(type: "text", nullable: false),
					Name = table.Column<string>(type: "text", nullable: false),
					Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					Role = table.Column<int>(type: "integer", nullable: false),
					SubscriptionId = table.Column<long>(type: "bigint", nullable: false),
					Balance = table.Column<double>(type: "double precision", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "Users",
				columns: new[] { "Id", "Balance", "Created", "Email", "Name", "PasswordHash", "Role", "SubscriptionId", "Updated" },
				values: new object[] { 1L, 1000000000.0, new DateTime(1111, 1, 1, 0, 1, 0, 0, DateTimeKind.Utc), "khamidov357@gmail.com", "Admin", "$2a$11$Evms7SKCcg1K.QZM84NL0OxvUJW5mp.5YQ4OPurBijb6Vp6uIdpxa", 0, 0L, new DateTime(2025, 5, 26, 15, 52, 49, 905, DateTimeKind.Utc).AddTicks(5729) });

			migrationBuilder.CreateIndex(
				name: "IX_Users_Email",
				table: "Users",
				column: "Email",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
