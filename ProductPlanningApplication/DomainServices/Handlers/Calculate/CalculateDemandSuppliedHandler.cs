using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Handlers.Calculate;

public class CalculateDemandSuppliedHandler
    : IRequestHandler<CalculateDemandSuppliedRequest, CalculateDemandSuppliedResponse>
{
    private readonly IProductPlanningCalculator _calculator;

    public CalculateDemandSuppliedHandler(IProductPlanningCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<CalculateDemandSuppliedResponse> Handle(
        CalculateDemandSuppliedRequest request, 
        CancellationToken cancellationToken)
    {
        var calculateDemandSuppliedDto = await _calculator
            .CalculateDemandSuppliedAsync(request.ProductId, request.Days, request.Date, cancellationToken);
        
        return new CalculateDemandSuppliedResponse(calculateDemandSuppliedDto);
    }
}