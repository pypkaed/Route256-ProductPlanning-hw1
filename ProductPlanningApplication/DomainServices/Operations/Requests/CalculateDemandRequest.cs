using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Operations.Requests;

public record struct CalculateDemandRequest(int ProductId, int Days)
    : IRequest<CalculateDemandResponse>;
