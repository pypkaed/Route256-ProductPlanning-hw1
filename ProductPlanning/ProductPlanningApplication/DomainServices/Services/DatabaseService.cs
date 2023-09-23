using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.DomainServices.Services.Interfaces;
using ProductPlanningApplication.Dtos;
using ProductPlanningApplication.Dtos.Mapping;
using ProductPlanningApplication.Exceptions;
using ProductPlanningApplication.Extensions;
using ProductPlanningDomain.Sales;
using ProductPlanningDomain.Sales.ValueObjects;

namespace ProductPlanningApplication.DomainServices.Services;

public class DatabaseService : IDatabaseService
{
    private readonly IProductPlanningDatabaseContext _databaseContext;

    public DatabaseService(IProductPlanningDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<SaleDto> CreateSale(
        int productId,
        DateOnly date,
        decimal sales,
        decimal stock,
        CancellationToken cancellationToken)
    {
        var sale = new Sale(
            productId,
            date,
            amountSold: new ProductAmount(sales),
            inStock: new ProductAmount(stock));

        bool saleExists = await _databaseContext.Sales
            .FindAsync(
                sale.GetCompositeKeys(),
                cancellationToken) is not null;
        
        if (saleExists)
        {
            throw ServiceException.RepeatingEntity(
                "Sale",
                sale.GetCompositeKeys());
        }
        
        _databaseContext.Sales.Add(sale);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return sale.AsDto();
    }

    public async Task<SeasonalCoefficientDto> CreateSeasonalCoefficient(
        int productId,
        int month,
        decimal coefficient,
        CancellationToken cancellationToken)
    {
        var coefficientModel = new Coefficient(coefficient);
        var seasonalCoefficient = new SeasonalCoefficient(productId, coefficientModel, month);

        bool coefficientExist = await _databaseContext
            .SeasonalCoefficients
            .FindAsync(
                seasonalCoefficient.GetCompositeKeys(),
                cancellationToken) is not null;
        
        if (coefficientExist)
        {
            throw ServiceException.RepeatingEntity(
                "SalesCoefficient",
                seasonalCoefficient.GetCompositeKeys());
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
    

    private bool AnySaleRecordExists(IEnumerable<Sale> sales)
    {
        var allKeys = sales
            .Select(e => e.GetCompositeKeys())
            .ToList();

        return _databaseContext.Sales
            .Any(e => allKeys.Contains(e.GetCompositeKeys()));
    }
    private bool AnySeasonalCoefficientRecordExists(IEnumerable<SeasonalCoefficient> coefficients)
    {
        var allKeys = coefficients
            .Select(coeff => coeff.GetCompositeKeys())
            .ToList();

        return _databaseContext.SeasonalCoefficients
            .Any(coeff => allKeys.Contains(coeff.GetCompositeKeys()));
    }
}