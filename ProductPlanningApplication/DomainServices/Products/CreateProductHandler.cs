using MediatR;
using ProductPlanningApplication.DataAccess;
using static ProductPlanningApplication.DomainServices.MediatROperations.Products.CreateProductOperation;

namespace ProductPlanningApplication.DomainServices.Products;

public class CreateProductHandler 
    : IRequestHandler<Request, Response>
{
    private IDatabaseContext _databaseContext;
    public CreateProductHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}