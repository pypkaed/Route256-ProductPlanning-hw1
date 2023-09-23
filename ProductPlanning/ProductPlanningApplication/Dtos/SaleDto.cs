namespace ProductPlanningApplication.Dtos;

public record SaleDto(
    int ProductId,
    DateOnly Date,
    decimal AmountSold,
    decimal InStock);