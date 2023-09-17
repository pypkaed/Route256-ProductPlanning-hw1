using ProductPlanningDomain.Products;

namespace ProductPlanningDomain.Sales;

public class Sale : IEquatable<Sale>
{
    public Sale(Guid id,
                Product product,
                DateTime date,
                ProductAmount amountSold)
    {
        Id = id;
        Product = product;
        Date = date;
        AmountSold = amountSold;
    }

    public Sale() { }
    
    public Guid Id { get; init; }
    public Product Product { get; init; }
    public DateTime Date { get; init; }
    public ProductAmount AmountSold { get; init; }

    public bool Equals(Sale? other)
        => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj)
        => Equals(obj as Sale);

    public override int GetHashCode()
        => Id.GetHashCode();
}