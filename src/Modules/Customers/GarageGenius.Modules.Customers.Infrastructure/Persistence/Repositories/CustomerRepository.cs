using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Exceptions;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Customers.Infrastructure.Persistence.Repositories;
internal class CustomerRepository(CustomersDbContext customersDbContext) : ICustomerRepository
{
	public async Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
	{
		await customersDbContext.Customers.AddAsync(customer, cancellationToken).ConfigureAwait(false);
		await customersDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
	}

	public async Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		Customer? customer = await customersDbContext.Customers
			.AsQueryable()
			.AsNoTracking()
			.SingleOrDefaultAsync(x => x.CustomerId == id, cancellationToken).ConfigureAwait(false) ??
		                     throw new CustomerNotFoundException(id);

		return customer;
	}

	public async Task<Customer?> GetCustomerByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
	{
		Customer? customer = await customersDbContext.Customers
			.AsQueryable()
			.AsNoTracking()
			.SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken).ConfigureAwait(false) ??
		                     throw new CustomerNotFoundException(userId);

		return customer;
	}

	public Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
	{
		customersDbContext.Customers.Update(customer);
        return customersDbContext.SaveChangesAsync(cancellationToken);
    }
}
