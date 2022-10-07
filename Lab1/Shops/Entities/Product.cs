using System.Reflection;

namespace Shops.Entities;

public class Product : IEquatable<Product>
{
    public Product(string name)
    {
        Name = name;
        Id = new Guid(name);
    }

    public Guid Id { get; }
    public string Name { get; }

    public bool Equals(Product? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}