using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Operations.Requests;

public record struct CalculateAverageDailySalesRequest(int ProductId) : IRequest<CalculateAverageDailySalesResponse>;
