using ProductPlanningDomain.Sales.ValueObjects;

namespace ProductPlanningDomain.Sales;

public class SeasonalCoefficient
{
    public SeasonalCoefficient(
        int productId,
        Coefficient coefficient,
        int month)
    {
        // TODO: validation
        ProductId = productId;
        Coefficient = coefficient;
        Month = month;
    }

    public SeasonalCoefficient()
    { }

    public int ProductId { get; init; }

    public Coefficient Coefficient { get; init; }

    public int Month { get; init; }
}