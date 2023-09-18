using MediatR;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CalculateAverageDailySalesOperation;

namespace ProductPlanningApplication.DomainServices.MediatRHandlers;

public class CalculateAverageDailySalesHandler : IRequestHandler<Request, Response>
{
    private readonly IProductPlanningCalculator _calculator;

    public CalculateAverageDailySalesHandler(IProductPlanningCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        return new Response(await _calculator.CalculateAverageDailySalesAsync(request.ProductId, cancellationToken));
    }
}