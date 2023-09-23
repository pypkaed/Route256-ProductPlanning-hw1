using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.Operations.Responses;

public record struct UploadSalesFileResponse(List<SaleDto> Sales);