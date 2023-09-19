using ProductPlanningApplication.DomainServices.MediatRHandlers.File;
using ProductPlanningDomain.Sales;
using ProductPlanningDomain.Sales.ValueObjects;

namespace ProductPlanningApplication.Dtos.Mapping;

public static class DtoMapping
{
    public static SeasonalCoefficientDto AsDto(this SeasonalCoefficient coefficient)
        => new SeasonalCoefficientDto(coefficient.ProductId, coefficient.Coefficient.Value, coefficient.Month);

    public static SaleDto AsDto(this Sale sale)
        => new SaleDto(
            sale.ProductId,
            sale.Date,
            sale.AmountSold.Value,
            sale.InStock.Value);

    public static List<SaleDto> AsDto(this IEnumerable<Sale> sales)
        => sales.Select(s => s.AsDto()).ToList();
    public static List<SeasonalCoefficientDto> AsDto(this IEnumerable<SeasonalCoefficient> coefficients)
        => coefficients.Select(s => s.AsDto()).ToList();

    public static IEnumerable<Sale> AsSale(this IEnumerable<SaleCsv> sales)
        => sales.Select(s => new Sale(
            s.Id,
            s.Date,
            new ProductAmount(s.Sales),
            new ProductAmount(s.Stock)));
    public static IEnumerable<SeasonalCoefficient> AsSeasonalCoefficient(this IEnumerable<SeasonalCoefficientCsv> coefficients)
        => coefficients.Select(s => new SeasonalCoefficient(
            s.Id,
            new Coefficient(s.Coeff),
            s.Month));
}