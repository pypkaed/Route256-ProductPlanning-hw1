using MediatR;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CalculateDemandOperation;

namespace ProductPlanningApplication.DomainServices.MediatRHandlers.Calculate;

public class CalculateDemandHandler : IRequestHandler<Request, Response>
{
    private readonly IProductPlanningCalculator _calculator;

    public CalculateDemandHandler(IProductPlanningCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        return new Response(await _calculator.CalculateDemandAsync(request.ProductId, request.Days, cancellationToken));
    }
}