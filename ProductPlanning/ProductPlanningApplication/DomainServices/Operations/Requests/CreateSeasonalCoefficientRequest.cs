using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Operations.Requests;

public record struct CreateSeasonalCoefficientRequest(
        int ProductId,
        decimal Coefficient,
        int Month) 
    : IRequest<CreateSeasonalCoefficientResponse>;
