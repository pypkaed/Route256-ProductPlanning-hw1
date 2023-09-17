using MediatR;
using ProductPlanningApplication.DataAccess;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CalculateSalesPredictionOperation;

namespace ProductPlanningApplication.DomainServices.Sales;

public class CalculateSalesPredictionHandler : IRequestHandler<Request, Response>
{
    private IDatabaseContext _databaseContext;

    public CalculateSalesPredictionHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}