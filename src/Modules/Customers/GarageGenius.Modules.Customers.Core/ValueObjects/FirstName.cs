using GarageGenius.Modules.Customers.Core.Exceptions;

namespace GarageGenius.Modules.Customers.Core.ValueObjects;
internal sealed class FirstName
{
    public string Value { get; }

    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            // IMPORTANT First name can be empty when customer is created by integration event
            Value = string.Empty;
            return;
        }

        if (Value?.Length > 50)
        {
            throw new InvalidFirstNameException(value);
        }

        Value = value;
    }

    public static implicit operator string(FirstName value)
    {
        return value?.Value;
    }

    public static implicit operator FirstName(string value)
    {
        return new FirstName(value);
    }

    public override string ToString()
    {
        return Value;
    }

    public bool Equals(FirstName? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((FirstName?)obj);
    }

    public override int GetHashCode()
    {
        return Value is not null ? Value.GetHashCode() : 0;
    }
}