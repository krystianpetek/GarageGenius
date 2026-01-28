using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Customers.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			name: "customers");

		migrationBuilder.CreateTable(
			name: "Customers",
			schema: "customers",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
				LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
				PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
				EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
				Created = table.Column<DateTime>(type: "datetime2", nullable: false),
				CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
				LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
				LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Customers", x => x.Id);
			});

		migrationBuilder.CreateIndex(
			name: "IX_Customers_EmailAddress",
			schema: "customers",
			table: "Customers",
			column: "EmailAddress",
			unique: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Customers",
			schema: "customers");
	}
}
