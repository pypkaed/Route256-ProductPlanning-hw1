using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Handlers.Calculate;

public class CalculateDemandHandler 
    : IRequestHandler<CalculateDemandRequest, CalculateDemandResponse>
{
    private readonly IProductPlanningCalculator _calculator;

    public CalculateDemandHandler(IProductPlanningCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<CalculateDemandResponse> Handle(
        CalculateDemandRequest request,
        CancellationToken cancellationToken)
    {
        var createDemandDto = await _calculator
            .CalculateDemandAsync(request.ProductId, request.Days, cancellationToken);
        
        return new CalculateDemandResponse(createDemandDto);
    }
}