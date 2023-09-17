namespace ProductPlanningDomain.Products;

public class Product : IEquatable<Product>
{
    public Product(Guid id, ProductAmount stock)
    {
        Id = id;
        Stock = stock;
    }
    
    public Product() { }
    
    public Guid Id { get; init; }
    public ProductAmount Stock { get; private set; }

    public void AddStock(ProductAmount stock)
        => Stock += stock;

    public void RemoveStock(ProductAmount stock)
        => Stock -= stock;

    public bool Equals(Product? other)
        => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj)
        => Equals(obj as Product);

    public override int GetHashCode()
        => Id.GetHashCode();
}