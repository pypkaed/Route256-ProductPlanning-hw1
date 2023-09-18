namespace ProductPlanningApplication.DomainServices.Dtos;

public record SaleDto(
    Guid Id,
    Guid ProductId,
    DateTime Date,
    int AmountSold,
    int InStock);