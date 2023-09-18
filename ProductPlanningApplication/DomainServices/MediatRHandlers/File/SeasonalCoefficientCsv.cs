namespace ProductPlanningApplication.DomainServices.MediatRHandlers.File;

public class SeasonalCoefficientCsv
{
    public int Id { get; set; }
    public int Month { get; set; }
    public decimal Coeff { get; set; }
}