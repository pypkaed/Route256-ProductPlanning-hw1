using ProductPlanningDomain.Sales.ValueObjects;
using ProductPlanningDomain.Validators;

namespace ProductPlanningDomain.Sales;

public class Sale : IEquatable<Sale>
{
    public Sale(int productId,
                DateTime date,
                ProductAmount amountSold,
                ProductAmount inStock)
    {
        ValueObjectValidator.ValidateProductId(productId);
        ValueObjectValidator.ValidateProductAmountStock(amountSold, inStock);
        ProductId = productId;
        Date = date;
        AmountSold = amountSold;
        InStock = inStock;
    }

    public Sale() { }
    
    public int ProductId { get; init; }
    public DateTime Date { get; init; }
    public ProductAmount AmountSold { get; init; }
    public ProductAmount InStock { get; init; }
    
    public bool Equals(Sale? other)
        => (other?.ProductId.Equals(ProductId) ?? false) &&
           (other?.Date.Equals(Date) ?? false);

    public override bool Equals(object? obj)
        => Equals(obj as Sale);

    public override int GetHashCode()
        => ProductId.GetHashCode() + Date.GetHashCode();
}