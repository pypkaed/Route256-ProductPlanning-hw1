using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Operations.Requests;

public record struct CalculateDemandSuppliedRequest(int ProductId, int Days, DateOnly Date) 
    : IRequest<CalculateDemandSuppliedResponse>;
