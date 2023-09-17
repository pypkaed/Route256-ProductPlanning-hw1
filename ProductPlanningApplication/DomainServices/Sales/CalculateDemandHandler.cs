using MediatR;
using ProductPlanningApplication.DataAccess;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CalculateDemandOperation;

namespace ProductPlanningApplication.DomainServices.Sales;

public class CalculateDemandHandler : IRequestHandler<Request, Response>
{
    private IDatabaseContext _databaseContext;

    public CalculateDemandHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}