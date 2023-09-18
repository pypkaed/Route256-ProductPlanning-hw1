using ProductPlanningDomain.Sales.ValueObjects;

namespace ProductPlanningDomain.Sales;

public class Sale : IEquatable<Sale>
{
    public Sale(Guid id,
                Guid productId,
                DateTime date,
                ProductAmount amountSold,
                ProductAmount inStock)
    {
        Id = id;
        ProductId = productId;
        Date = date;
        AmountSold = amountSold;
        InStock = inStock;
    }

    public Sale() { }
    
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public DateTime Date { get; init; }
    public ProductAmount AmountSold { get; init; }
    public ProductAmount InStock { get; init; }

    public bool Equals(Sale? other)
        => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj)
        => Equals(obj as Sale);

    public override int GetHashCode()
        => Id.GetHashCode();
}