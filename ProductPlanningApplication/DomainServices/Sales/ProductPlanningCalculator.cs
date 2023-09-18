using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.Extensions;
using ProductPlanningDomain.Sales.ValueObjects;

namespace ProductPlanningApplication.DomainServices.Sales;

public class ProductPlanningCalculator : IProductPlanningCalculator
{
    private readonly IProductPlanningDatabaseContext _databaseContext;

    public ProductPlanningCalculator(IProductPlanningDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<decimal> CalculateAverageDailySalesAsync(Guid productId, CancellationToken cancellationToken)
    {
        var sales = await _databaseContext.Sales
            .Where(s => s.ProductId == productId)
            .ToListAsync(cancellationToken);

        var amountSold = sales.Select(s => s.AmountSold.Value).Sum();
        var numOfDaysInStock = sales.Count(s => s.InStock.Value > 0);
        var averageDailySales = amountSold / numOfDaysInStock;
        
        return averageDailySales;
    }

    public async Task<decimal> CalculateSalesPredictionAsync(Guid productId, int numOfDays, CancellationToken cancellationToken)
    {
        var averageDailySales = await CalculateAverageDailySalesAsync(productId, cancellationToken);
        var currentDate = DateTime.Now;
        var monthsInFuture = Enumerable.Range(0, numOfDays)
            .Select(offset => currentDate.AddDays(offset))
            .GroupBy(date => date.Month)
            .Select(group => new
            {
                Month = group.Key,
                DaysInMonth = (decimal)group.Count()
            });

        Coefficient coefficient;
        foreach (var month in monthsInFuture)
        {
            var seasonalCoefficient = await _databaseContext.SeasonalCoefficients.GetEntityAsync(
                new object?[]{ productId, month.Month },
                cancellationToken);
            coefficient += seasonalCoefficient.Coefficient * month.DaysInMonth;
        }

        var salesPrediction = averageDailySales * coefficient.Value;
        return salesPrediction;
    }

    public async Task<decimal> CalculateDemandAsync(Guid productId, int numOfDays, CancellationToken cancellationToken)
    {
        var lastSaleProductEntry = await _databaseContext.Sales
            .Where(s => s.ProductId == productId)
            .OrderBy(s => s.Date)
            .LastAsync(cancellationToken);
        var productAmountInStock = lastSaleProductEntry.InStock;

        var salesPrediction = await CalculateSalesPredictionAsync(productId, numOfDays, cancellationToken);
        var demand = salesPrediction - productAmountInStock.Value;

        return demand;
    }
}