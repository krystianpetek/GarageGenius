using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Customers.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class FirstLastNameChanges : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			name: "FirstName",
			schema: "customers",
			table: "Customers",
			type: "nvarchar(50)",
			maxLength: 50,
			nullable: true,
			oldClrType: typeof(string),
			oldType: "nvarchar(30)",
			oldMaxLength: 30,
			oldNullable: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			name: "FirstName",
			schema: "customers",
			table: "Customers",
			type: "nvarchar(30)",
			maxLength: 30,
			nullable: true,
			oldClrType: typeof(string),
			oldType: "nvarchar(50)",
			oldMaxLength: 50,
			oldNullable: true);
	}
}
