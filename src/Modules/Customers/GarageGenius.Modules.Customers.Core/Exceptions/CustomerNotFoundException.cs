﻿using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Customers.Core.Exceptions;
internal sealed class CustomerNotFoundException : GarageGeniusException
{
	public CustomerNotFoundException(Guid id) : base($"Customer with ID: '{id}' was not found.") { }
}
