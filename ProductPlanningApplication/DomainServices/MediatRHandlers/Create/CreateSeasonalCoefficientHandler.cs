using MediatR;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CreateSeasonalCoefficientOperation;

namespace ProductPlanningApplication.DomainServices.MediatRHandlers.Create;

public class CreateSeasonalCoefficientHandler : IRequestHandler<Request, Response>
{
    private readonly IDatabaseService _databaseService;

    public CreateSeasonalCoefficientHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        return new Response(await _databaseService.CreateSeasonalCoefficient(
            request.ProductId, request.Month, request.Coefficient, cancellationToken));
    }
}