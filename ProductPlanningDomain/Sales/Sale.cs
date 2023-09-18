using ProductPlanningDomain.Sales.ValueObjects;

namespace ProductPlanningDomain.Sales;

public class Sale : IEquatable<Sale>
{
    public Sale(Guid id,
                int productId,
                DateTime date,
                ProductAmount amountSold,
                ProductAmount inStock)
    {
        ValidateProductId(productId);
        ValidateProductAmount(amountSold, inStock);
        Id = id;
        ProductId = productId;
        Date = date;
        AmountSold = amountSold;
        InStock = inStock;
    }

    public Sale() { }
    
    public Guid Id { get; init; }
    public int ProductId { get; init; }
    public DateTime Date { get; init; }
    public ProductAmount AmountSold { get; init; }
    public ProductAmount InStock { get; init; }

    private void ValidateProductId(int id)
    {
        if (id <= 0)
            throw new Exception();
    }

    private void ValidateProductAmount(
        ProductAmount amountSold,
        ProductAmount inStock)
    {
        if (inStock.Value < amountSold.Value)
            throw new Exception();
    }

    public bool Equals(Sale? other)
        => (other?.ProductId.Equals(ProductId) ?? false) &&
           (other?.Date.Equals(Date) ?? false);

    public override bool Equals(object? obj)
        => Equals(obj as Sale);

    public override int GetHashCode()
        => ProductId.GetHashCode() + Date.GetHashCode();
}