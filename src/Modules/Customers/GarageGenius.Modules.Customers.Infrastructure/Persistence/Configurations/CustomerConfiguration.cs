using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Types;
using GarageGenius.Modules.Customers.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Customers.Infrastructure.Persistence.Configurations;
internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		builder.HasIndex(customer => customer.CustomerId).IsUnique();

		builder.Property(customer => customer.CustomerId)
			.IsRequired()
			.HasConversion(customerId => customerId.Value,
				x => new CustomerId(x));

		builder.Property(customer => customer.UserId)
			.HasConversion(userId => userId!.Value,
				x => new UserId(x));

		builder.Property(customer => customer.FirstName)
			.HasMaxLength(50)
			.HasConversion(firstName => firstName!.Value,
				x => new FirstName(x));

		builder.Property(customer => customer.LastName)
			.HasMaxLength(50)
			.HasConversion(lastName => lastName!.Value,
				x => new LastName(x));

		builder.Property(customer => customer.EmailAddress)
			.IsRequired()
			.HasMaxLength(100)
			.HasConversion(emailAddress => emailAddress.Value,
				x => new EmailAddress(x));

		builder.Property(x => x.PhoneNumber)
			.HasMaxLength(12)
			.HasConversion(phoneNumber => phoneNumber!.Value,
				x => new PhoneNumber(x));
	}
}
