using MediatR;
using ProductPlanningApplication.DataAccess;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CreateSaleOperation;

namespace ProductPlanningApplication.DomainServices.Sales;

public class CreateSaleHandler : IRequestHandler<Request, Response>
{
    private IDatabaseContext _databaseContext;

    public CreateSaleHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}