using MediatR;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CreateSaleOperation;

namespace ProductPlanningApplication.DomainServices.MediatRHandlers.Create;

public class CreateSaleHandler : IRequestHandler<Request, Response>
{
    private readonly IDatabaseService _databaseService;

    public CreateSaleHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        return new Response(await _databaseService.CreateSale(
            request.ProductId,
            request.Date.Date,
            request.Sales, 
            request.Stock, 
            cancellationToken));
    }
}