using MediatR;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.DomainServices.Dtos.Mapping;
using ProductPlanningDomain.Products;
using static ProductPlanningApplication.DomainServices.MediatROperations.Products.CreateProductOperation;

namespace ProductPlanningApplication.DomainServices.Products;

public class CreateProductHandler 
    : IRequestHandler<Request, Response>
{
    private readonly IProductPlanningDatabaseContext _databaseContext;
    public CreateProductHandler(IProductPlanningDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var product = new Product(Guid.NewGuid());
        
        _databaseContext.Products.Add(product);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new Response(product.AsDto());
    }
}