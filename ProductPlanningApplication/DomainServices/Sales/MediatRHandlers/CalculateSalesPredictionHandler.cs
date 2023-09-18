using MediatR;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CalculateSalesPredictionOperation;

namespace ProductPlanningApplication.DomainServices.Sales.MediatRHandlers;

public class CalculateSalesPredictionHandler : IRequestHandler<Request, Response>
{
    private readonly IProductPlanningCalculator _calculator;

    public CalculateSalesPredictionHandler(IProductPlanningCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        return new Response(
            await _calculator.CalculateSalesPredictionAsync(request.ProductId, request.Days, cancellationToken));
    }
}