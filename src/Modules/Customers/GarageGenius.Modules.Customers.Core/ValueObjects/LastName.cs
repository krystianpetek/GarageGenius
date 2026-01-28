using GarageGenius.Modules.Customers.Core.Exceptions;

namespace GarageGenius.Modules.Customers.Core.ValueObjects;
internal sealed class LastName
{
    public string Value { get; }

    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            // IMPORTANT Last name can be empty when customer is created by integration event
            Value = string.Empty;
            return;
        }

        if (Value?.Length > 50)
        {
            throw new InvalidLastNameException(value);
        }

        Value = value;
    }

    public static implicit operator string(LastName value)
    {
        return value?.Value;
    }

    public static implicit operator LastName(string value)
    {
        return new LastName(value);
    }

    public override string ToString()
    {
        return Value;
    }

    public bool Equals(LastName? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((LastName?)obj);
    }

    public override int GetHashCode()
    {
        return Value is not null ? Value.GetHashCode() : 0;
    }
}