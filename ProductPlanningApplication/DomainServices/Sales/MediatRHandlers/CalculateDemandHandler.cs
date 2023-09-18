using MediatR;
using ProductPlanningApplication.DataAccess;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CalculateDemandOperation;

namespace ProductPlanningApplication.DomainServices.Sales.MediatRHandlers;

public class CalculateDemandHandler : IRequestHandler<Request, Response>
{
    private IProductPlanningDatabaseContext _productPlanningDatabaseContext;

    public CalculateDemandHandler(IProductPlanningDatabaseContext productPlanningDatabaseContext)
    {
        _productPlanningDatabaseContext = productPlanningDatabaseContext;
    }

    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}