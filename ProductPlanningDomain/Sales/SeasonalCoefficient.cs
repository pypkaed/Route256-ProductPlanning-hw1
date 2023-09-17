using ProductPlanningDomain.Products;

namespace ProductPlanningDomain.Sales;

public class SeasonalCoefficient
{
    public Guid ProductId { get; }
    public Coefficient Coefficient { get; }
    public int Month { get; }

    public SeasonalCoefficient(Guid productId, Coefficient coefficient, int month)
    {
        ProductId = productId;
        Coefficient = coefficient;
        Month = month;
    }
}