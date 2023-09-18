namespace ProductPlanningApplication.Exceptions;

public class CalculationsException : Exception
{
    private CalculationsException(string message) : base(message) { }
    
    public static CalculationsException NotEnoughStock(int productId)
        => new CalculationsException($"Product with ID {productId} was never in stock.");
}