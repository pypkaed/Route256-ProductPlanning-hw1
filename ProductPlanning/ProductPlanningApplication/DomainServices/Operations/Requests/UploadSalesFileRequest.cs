using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Responses;

namespace ProductPlanningApplication.DomainServices.Operations.Requests;

public record struct UploadSalesFileRequest(Stream FileStream) : IRequest<UploadSalesFileResponse>;