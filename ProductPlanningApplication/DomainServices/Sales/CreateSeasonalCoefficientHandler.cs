using MediatR;
using ProductPlanningApplication.DataAccess;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CreateSeasonalCoefficientHandler;

namespace ProductPlanningApplication.DomainServices.Sales;

public class CreateSeasonalCoefficientHandler : IRequestHandler<Request, Response>
{
    private IDatabaseContext _databaseContext;

    public CreateSeasonalCoefficientHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}