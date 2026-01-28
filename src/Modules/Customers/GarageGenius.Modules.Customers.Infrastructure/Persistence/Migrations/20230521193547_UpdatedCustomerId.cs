using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Customers.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class UpdatedCustomerId : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			name: "Id",
			schema: "customers",
			table: "Customers",
			newName: "CustomerId");

		migrationBuilder.RenameIndex(
			name: "IX_Customers_Id",
			schema: "customers",
			table: "Customers",
			newName: "IX_Customers_CustomerId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			name: "CustomerId",
			schema: "customers",
			table: "Customers",
			newName: "Id");

		migrationBuilder.RenameIndex(
			name: "IX_Customers_CustomerId",
			schema: "customers",
			table: "Customers",
			newName: "IX_Customers_Id");
	}
}
