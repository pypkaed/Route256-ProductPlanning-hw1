using MediatR;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CalculateDemandSuppliedOperation;

namespace ProductPlanningApplication.DomainServices.MediatRHandlers.Calculate;

public class CalculateDemandSuppliedHandler : IRequestHandler<Request, Response>
{
    private readonly IProductPlanningCalculator _calculator;

    public CalculateDemandSuppliedHandler(IProductPlanningCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        return new Response(await _calculator.CalculateDemandSuppliedAsync(request.ProductId, request.Days, request.Date, cancellationToken));
    }
}