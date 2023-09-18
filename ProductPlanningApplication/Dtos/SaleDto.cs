namespace ProductPlanningApplication.Dtos;

public record SaleDto(
    int ProductId,
    DateTime Date,
    decimal AmountSold,
    decimal InStock);