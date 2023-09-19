using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.Dtos;
using ProductPlanningApplication.Dtos.Mapping;
using ProductPlanningApplication.Exceptions;
using ProductPlanningDomain.Sales;
using ProductPlanningDomain.Sales.ValueObjects;

namespace ProductPlanningApplication.DomainServices;

public class DatabaseService : IDatabaseService
{
    private readonly IProductPlanningDatabaseContext _databaseContext;

    public DatabaseService(IProductPlanningDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<SaleDto> CreateSale(
        int productId,
        DateTime date,
        decimal sales,
        decimal stock,
        CancellationToken cancellationToken)
    {
        var sale = new Sale(
            productId,
            date,
            amountSold: new ProductAmount(sales),
            inStock: new ProductAmount(stock));

        if (await _databaseContext.Sales
                .FindAsync(
                    new object?[] { sale.ProductId, sale.Date },
                    cancellationToken) is not null)
        {
            throw ServiceException.RepeatingEntity("Sale", new object?[] { sale.ProductId, sale.Date });
        }
        
        _databaseContext.Sales.Add(sale);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return sale.AsDto();
    }

    public async Task<SeasonalCoefficientDto> CreateSeasonalCoefficient(
        int productId,
        int month,
        decimal coeff,
        CancellationToken cancellationToken)
    {
        var coefficient = new Coefficient(coeff);
        var seasonalCoefficient = new SeasonalCoefficient(productId, coefficient, month);
        
        if (await _databaseContext.SeasonalCoefficients
                .FindAsync(new object?[]
                {
                    seasonalCoefficient.ProductId,
                    seasonalCoefficient.Month
                }, cancellationToken) is not null)
        {
            throw ServiceException.RepeatingEntity("SalesCoefficient" ,
                new object?[] { 
                    seasonalCoefficient.ProductId,
                    seasonalCoefficient.Month });
        }

        _databaseContext.SeasonalCoefficients.Add(seasonalCoefficient);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return seasonalCoefficient.AsDto();
    }

    public async Task<List<SaleDto>> CreateSalesBulk(
        IEnumerable<Sale> sales,
        CancellationToken cancellationToken)
    {
        var salesList = sales.ToList();
        if (AnySaleRecordExists(salesList))
            throw ServiceException.RepeatingEntity("Sale");

        _databaseContext.Sales.AddRange(salesList);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return salesList.AsDto();
    }

    public async Task<List<SeasonalCoefficientDto>> CreateSeasonalCoefficientsBulk(
        IEnumerable<SeasonalCoefficient> coefficients,
        CancellationToken cancellationToken)
    {
        var coefficientList = coefficients.ToList();
        if (AnySeasonalCoefficientRecordExists(coefficientList))
            throw ServiceException.RepeatingEntity("Sale");

        _databaseContext.SeasonalCoefficients.AddRange(coefficientList);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return coefficientList.AsDto();
    }

    public bool AnySaleRecordExists(IEnumerable<Sale> sales)
    {
        var keys = sales.Select(e => new object?[]
        {
            e.ProductId,
            e.Date
        }).ToList();

        return _databaseContext.Sales.Any(e => keys.Contains(new object?[]
        {
            e.ProductId,
            e.Date
        }));
    }
    public bool AnySeasonalCoefficientRecordExists(IEnumerable<SeasonalCoefficient> coefficients)
    {
        var keys = coefficients.Select(e => new object?[]
        {
            e.ProductId,
            e.Month
        }).ToList();

        return _databaseContext.SeasonalCoefficients.Any(e => keys.Contains(new object?[]
        {
            e.ProductId,
            e.Month
        }));
    }
}