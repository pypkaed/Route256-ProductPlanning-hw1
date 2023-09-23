using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Handlers.Create;

public class CreateSeasonalCoefficientHandler 
    : IRequestHandler<CreateSeasonalCoefficientRequest, CreateSeasonalCoefficientResponse>
{
    private readonly IDatabaseService _databaseService;

    public CreateSeasonalCoefficientHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    public async Task<CreateSeasonalCoefficientResponse> Handle(
        CreateSeasonalCoefficientRequest request, 
        CancellationToken cancellationToken)
    {
        var seasonalCoefficientDto = await _databaseService.CreateSeasonalCoefficient(
            request.ProductId, request.Month, request.Coefficient, cancellationToken);
        
        return new CreateSeasonalCoefficientResponse(seasonalCoefficientDto);
    }
}