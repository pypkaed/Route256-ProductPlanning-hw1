using MediatR;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.Dtos.Mapping;
using ProductPlanningApplication.Exceptions;
using ProductPlanningDomain.Sales;
using ProductPlanningDomain.Sales.ValueObjects;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CreateSeasonalCoefficientOperation;

namespace ProductPlanningApplication.DomainServices.MediatRHandlers;

public class CreateSeasonalCoefficientHandler : IRequestHandler<Request, Response>
{
    private readonly IProductPlanningDatabaseContext _databaseContext;

    public CreateSeasonalCoefficientHandler(IProductPlanningDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var coefficient = new Coefficient(request.Coefficient);
        var seasonalCoefficient = new SeasonalCoefficient(request.ProductId, coefficient, request.Month);
        
        // TODO: custom exceptions
        if (await _databaseContext.SeasonalCoefficients
                .FindAsync(new object?[]
                {
                    seasonalCoefficient.ProductId,
                    seasonalCoefficient.Month
                }, cancellationToken)
            is not null)
        {
            throw ServiceException.RepeatingEntity("SalesCoefficient" ,
                new object?[] { 
                seasonalCoefficient.ProductId,
                seasonalCoefficient.Month });
        }

        _databaseContext.SeasonalCoefficients.Add(seasonalCoefficient);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new Response(seasonalCoefficient.AsDto());
    }
}