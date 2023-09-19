using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.DomainServices;
using ProductPlanningApplication.Exceptions;
using ProductPlanningDataAccess;
using Xunit;


namespace Tests.Application;

public class DomainServiceExceptionsTest
{
    private readonly IProductPlanningDatabaseContext _context;
    private readonly IDatabaseService _databaseService;
    private readonly IProductPlanningCalculator _calculator;

    public DomainServiceExceptionsTest()
    {
        DbContextOptions<ProductPlanningDatabaseContext> options =
            new DbContextOptionsBuilder<ProductPlanningDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
        _context = new ProductPlanningDatabaseContext(options);

        _databaseService = new DatabaseService(_context);
        _calculator = new ProductPlanningCalculator(_context);
    }

    [Fact]
    public async Task TryGetEntity_ThrowServiceException()
    {
        await Assert.ThrowsAsync<ServiceException>(async () =>
        {
            await _calculator.CalculateSalesPredictionAsync(1, 15, CancellationToken.None);
        });
    }

    [Fact]
    public async Task CreateSale_ThrowServiceException()
    {
        await Assert.ThrowsAsync<ServiceException>(async () =>
        {
            await _databaseService.CreateSale(1, DateTime.Now, 10, 50, CancellationToken.None);
            await _databaseService.CreateSale(1, DateTime.Now, 50, 100, CancellationToken.None);
        });
        
        await Assert.ThrowsAsync<ServiceException>(async () =>
        {
            await _databaseService.CreateSale(1, DateTime.Now, 10, 50, CancellationToken.None);
            await _databaseService.CreateSale(1, DateTime.Now.AddMinutes(15), 50, 100, CancellationToken.None);
        });
    }

    [Fact]
    public async Task CalculateAds_ThrowCalculationsException()
    {
        await _databaseService.CreateSale(1, DateTime.Now, 0, 0, CancellationToken.None);
        await _databaseService.CreateSale(1, DateTime.Now.AddDays(1), 0, 0, CancellationToken.None);

        await Assert.ThrowsAsync<CalculationsException>(async () =>
        {
            await _calculator.CalculateAverageDailySalesAsync(1, CancellationToken.None);
        });
    }
}