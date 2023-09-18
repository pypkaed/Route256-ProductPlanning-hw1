using MediatR;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.Dtos.Mapping;
using ProductPlanningApplication.Exceptions;
using ProductPlanningDomain.Sales;
using ProductPlanningDomain.Sales.ValueObjects;
using static ProductPlanningApplication.DomainServices.MediatROperations.Sales.CreateSaleOperation;

namespace ProductPlanningApplication.DomainServices.MediatRHandlers;

public class CreateSaleHandler : IRequestHandler<Request, Response>
{
    private readonly IProductPlanningDatabaseContext _databaseContext;

    public CreateSaleHandler(IProductPlanningDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var sale = new Sale(
            request.ProductId,
            request.Date.Date,
            amountSold: new ProductAmount(request.Sales),
            inStock: new ProductAmount(request.Stock));

        if (await _databaseContext.Sales
                .FindAsync(
                    new object?[] { sale.ProductId, sale.Date },
                    cancellationToken)
            is not null)
        {
            throw ServiceException.RepeatingEntity("Sale", new object?[] { sale.ProductId, sale.Date });
        }
        
        _databaseContext.Sales.Add(sale);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new Response(sale.AsDto());
    }
}