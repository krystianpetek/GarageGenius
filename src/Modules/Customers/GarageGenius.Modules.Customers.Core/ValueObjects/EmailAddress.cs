using GarageGenius.Modules.Customers.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Customers.Core.ValueObjects;
internal sealed class EmailAddress
{
    public string Value { get; }

    public EmailAddress(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidEmailException(email);
        }

        if (email.Length > 100)
        {
            throw new InvalidEmailException(email);
        }

        if (!new EmailAddressAttribute().IsValid(email))
        {
            throw new InvalidEmailException(email);
        }

        Value = email.ToLowerInvariant();
    }

    public static implicit operator string(EmailAddress email)
    {
        return email.Value;
    }

    public static implicit operator EmailAddress(string email)
    {
        if (email is null) return null;
        return new EmailAddress(email);
    }

    public bool Equals(EmailAddress? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public override int GetHashCode()
    {
        return Value is not null ? Value.GetHashCode() : 0;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((EmailAddress?)obj);
    }
}
