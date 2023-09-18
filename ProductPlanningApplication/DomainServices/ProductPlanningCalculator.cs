using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.Exceptions;
using ProductPlanningApplication.Extensions;
using ProductPlanningDomain.Sales.ValueObjects;

namespace ProductPlanningApplication.DomainServices;

public class ProductPlanningCalculator : IProductPlanningCalculator
{
    private readonly IProductPlanningDatabaseContext _databaseContext;

    public ProductPlanningCalculator(IProductPlanningDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<decimal> CalculateAverageDailySalesAsync(int productId, CancellationToken cancellationToken)
    {
        var sales = await _databaseContext.Sales
            .Where(s => s.ProductId == productId)
            .ToListAsync(cancellationToken);
        if (sales.Count == 0)
            throw ServiceException.DbSetEntityNotFound(
                _databaseContext.Sales.EntityType,
                new object?[] { productId });

        var amountSold = sales.Select(s => s.AmountSold.Value).Sum();
        var numOfDaysInStock = sales.Count(s => s.InStock.Value > 0);
        if (numOfDaysInStock == 0)
            throw CalculationsException.NotEnoughStock(productId);
        
        var averageDailySales = amountSold / numOfDaysInStock;
        
        return averageDailySales;
    }

    public async Task<decimal> CalculateSalesPredictionAsync(int productId, int numOfDays, CancellationToken cancellationToken)
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

        var coefficient = new Coefficient(0);
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

    public async Task<decimal> CalculateDemandSuppliedAsync(int productId, int numOfDays, DateTime supplyDate, CancellationToken cancellationToken)
    {
        var supplySaleProductEntry = await _databaseContext.Sales
            .Where(s => s.ProductId == productId)
            .OrderBy(s => s.Date)
            .FirstOrDefaultAsync(
                // check only by date, without time
                s => s.Date.Date.Equals(supplyDate.Date), 
                cancellationToken) 
                                     ?? throw ServiceException.DbSetEntityNotFound(
                                         _databaseContext.Sales.EntityType,
                                         new object?[] { productId });;
        var productAmountInStock = supplySaleProductEntry.InStock;

        var salesPrediction = await CalculateSalesPredictionAsync(productId, numOfDays, cancellationToken);
        var demand = salesPrediction - productAmountInStock.Value;

        return demand > 0 ? demand : 0;
    }
    
    public async Task<decimal> CalculateDemandAsync(int productId, int numOfDays, CancellationToken cancellationToken)
    {
        var lastSaleProductEntry = await _databaseContext.Sales
            .Where(s => s.ProductId == productId)
            .OrderBy(s => s.Date)
            .LastAsync(cancellationToken);
        
        if (lastSaleProductEntry is null)
            throw ServiceException.DbSetEntityNotFound(
                _databaseContext.Sales.EntityType,
                new object?[] { productId });
        
        var productAmountInStock = lastSaleProductEntry.InStock;

        var salesPrediction = await CalculateSalesPredictionAsync(productId, numOfDays, cancellationToken);
        var demand = salesPrediction - productAmountInStock.Value;

        return demand > 0 ? demand : 0;
    }
}