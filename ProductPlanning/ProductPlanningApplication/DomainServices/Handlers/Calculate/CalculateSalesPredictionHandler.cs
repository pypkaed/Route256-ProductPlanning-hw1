using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.DomainServices.Operations.Responses;
using ProductPlanningApplication.DomainServices.Services.Interfaces;

namespace ProductPlanningApplication.DomainServices.Handlers.Calculate;

public class CalculateSalesPredictionHandler 
    : IRequestHandler<CalculateSalesPredictionRequest, CalculateSalesPredictionResponse>
{
    private readonly IProductPlanningCalculator _calculator;

    public CalculateSalesPredictionHandler(IProductPlanningCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<CalculateSalesPredictionResponse> Handle(
        CalculateSalesPredictionRequest request,
        CancellationToken cancellationToken)
    {
        var calculateSalesPredictionDto = await _calculator
            .CalculateSalesPrediction(request.ProductId, request.Days, cancellationToken);
        
        return new CalculateSalesPredictionResponse(calculateSalesPredictionDto);
    }
}