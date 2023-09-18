namespace ProductPlanningApplication.DomainServices.Sales;

public interface IProductPlanningCalculator
{
    Task<decimal> CalculateAverageDailySalesAsync(Guid productId, CancellationToken cancellationToken);
    Task<decimal> CalculateSalesPredictionAsync(Guid productId, int numOfDays, CancellationToken cancellationToken);
    Task<decimal> CalculateDemandAsync(Guid productId, int numOfDays, CancellationToken cancellationToken);
}