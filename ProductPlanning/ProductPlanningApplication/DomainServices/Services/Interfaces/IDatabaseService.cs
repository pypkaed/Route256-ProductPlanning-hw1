using ProductPlanningApplication.Dtos;
using ProductPlanningDomain.Sales;

namespace ProductPlanningApplication.DomainServices.Services.Interfaces;

public interface IDatabaseService
{
    Task<SaleDto> CreateSale(
        int productId,
        DateOnly date,
        decimal sales,
        decimal stock,
        CancellationToken cancellationToken);

    Task<SeasonalCoefficientDto> CreateSeasonalCoefficient(
        int productId,
        int month,
        decimal coefficient,
        CancellationToken cancellationToken);

    public Task<List<SaleDto>> CreateSalesBulk(
        IEnumerable<Sale> sales,
        CancellationToken cancellationToken);
    public Task<List<SeasonalCoefficientDto>> CreateSeasonalCoefficientsBulk(
        IEnumerable<SeasonalCoefficient> coefficients,
        CancellationToken cancellationToken);
}