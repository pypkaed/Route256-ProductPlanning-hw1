using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Operations.Requests;

public record struct CalculateSalesPredictionRequest(int ProductId, int Days) 
    : IRequest<CalculateSalesPredictionResponse>;
