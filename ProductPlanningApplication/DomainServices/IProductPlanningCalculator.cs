namespace ProductPlanningApplication.DomainServices;

public interface IProductPlanningCalculator
{
    Task<decimal> CalculateAverageDailySalesAsync(int productId, CancellationToken cancellationToken);
    Task<decimal> CalculateSalesPredictionAsync(int productId, int numOfDays, CancellationToken cancellationToken);

    Task<decimal> CalculateSalesPredictionAsync(
        int productId,
        int numOfDays, 
        DateTime currentDate,
        CancellationToken cancellationToken);
    Task<decimal> CalculateDemandSuppliedAsync(int productId, int numOfDays, DateTime supplyDate, CancellationToken cancellationToken);
    Task<decimal> CalculateDemandAsync(int productId, int numOfDays, CancellationToken cancellationToken);
}