using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Customers.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class IndexChangedFromEmailToId : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex(
			name: "IX_Customers_EmailAddress",
			schema: "customers",
			table: "Customers");

		migrationBuilder.CreateIndex(
			name: "IX_Customers_Id",
			schema: "customers",
			table: "Customers",
			column: "Id",
			unique: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex(
			name: "IX_Customers_Id",
			schema: "customers",
			table: "Customers");

		migrationBuilder.CreateIndex(
			name: "IX_Customers_EmailAddress",
			schema: "customers",
			table: "Customers",
			column: "EmailAddress",
			unique: true);
	}
}
