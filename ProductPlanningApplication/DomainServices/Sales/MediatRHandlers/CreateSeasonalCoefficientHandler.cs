using MediatR;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.DomainServices.Dtos.Mapping;
using ProductPlanningDomain.Sales;
using ProductPlanningDomain.Sales.ValueObjects;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CreateSeasonalCoefficientHandler;

namespace ProductPlanningApplication.DomainServices.Sales.MediatRHandlers;

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

        _databaseContext.SeasonalCoefficients.Add(seasonalCoefficient);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new Response(seasonalCoefficient.AsDto());
    }
}