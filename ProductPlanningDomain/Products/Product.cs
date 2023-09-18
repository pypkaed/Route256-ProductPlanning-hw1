namespace ProductPlanningDomain.Products;

public class Product : IEquatable<Product>
{
    public Product(Guid id)
    {
        Id = id;
    }
    
    public Product() { }
    
    public Guid Id { get; init; }

    public bool Equals(Product? other)
        => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj)
        => Equals(obj as Product);

    public override int GetHashCode()
        => Id.GetHashCode();
}