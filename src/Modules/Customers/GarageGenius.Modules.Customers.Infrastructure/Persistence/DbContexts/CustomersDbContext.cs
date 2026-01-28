using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Customers.Infrastructure.Persistence.DbContexts;
internal class CustomersDbContext(
	DbContextOptions<CustomersDbContext> dbContextOptions,
	AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
	: DbContext(dbContextOptions)
{
	public required DbSet<Customer> Customers { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.AddInterceptors(auditableEntitySaveChangesInterceptor);
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("customers");

		// This line is the key to apply all configurations in the assembly e.g. CustomerConfiguration
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}
