using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Operations.Requests;

public record struct CreateSaleRequest(int ProductId, DateOnly Date, decimal Sales, decimal Stock) 
    : IRequest<CreateSaleResponse>;
