using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.DomainServices.Operations.Responses;
using ProductPlanningApplication.DomainServices.Services.Interfaces;

namespace ProductPlanningApplication.DomainServices.Handlers.Create;

public class CreateSaleHandler
    : IRequestHandler<CreateSaleRequest, CreateSaleResponse>
{
    private readonly IDatabaseService _databaseService;

    public CreateSaleHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<CreateSaleResponse> Handle(
        CreateSaleRequest request,
        CancellationToken cancellationToken)
    {
        var saleDto = await _databaseService.CreateSale(
            request.ProductId,
            request.Date, 
            request.Sales,
            request.Stock,
            cancellationToken);
        
        return new CreateSaleResponse(saleDto);
    }
}