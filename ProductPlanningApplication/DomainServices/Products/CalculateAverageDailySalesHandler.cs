using MediatR;
using ProductPlanningApplication.DataAccess;
using static ProductPlanningApplication.DomainServices.MediatROperations.Products.CalculateAverageDailySalesOperation;

namespace ProductPlanningApplication.DomainServices.Products;

public class CalculateAverageDailySalesHandler : IRequestHandler<Request, Response>
{
    private IDatabaseContext _databaseContext;

    public CalculateAverageDailySalesHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}