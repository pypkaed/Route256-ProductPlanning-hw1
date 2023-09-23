using ProductPlanningDomain.Sales;

namespace ProductPlanningApplication.Extensions;

public static class CompositeKeysExtension
{
    public static object?[] GetCompositeKeys(this Sale sale)
        => new object?[]
        {
            sale.ProductId,
            sale.Date
        };
    
    public static object?[] GetCompositeKeys(this SeasonalCoefficient seasonalCoefficient)
        => new object?[] 
        { 
            seasonalCoefficient.ProductId,
            seasonalCoefficient.Month 
        };
}