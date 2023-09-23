using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Handlers.Calculate;

public class CalculateAverageDailySalesHandler :
    IRequestHandler<CalculateAverageDailySalesRequest, CalculateAverageDailySalesResponse>
{
    private readonly IProductPlanningCalculator _calculator;

    public CalculateAverageDailySalesHandler(IProductPlanningCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<CalculateAverageDailySalesResponse> Handle(
        CalculateAverageDailySalesRequest request,
        CancellationToken cancellationToken)
    {
        var averageDailySalesDto = await _calculator
            .CalculateAverageDailySalesAsync(request.ProductId, cancellationToken);
        
        return new CalculateAverageDailySalesResponse(averageDailySalesDto);
    }
}