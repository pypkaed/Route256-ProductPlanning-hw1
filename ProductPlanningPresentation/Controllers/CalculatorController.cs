using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.MediatROperations.Sales;
using ProductPlanningPresentation.Models;


namespace ProductPlanningPresentation.Controllers;

[ApiController]
[Route("api")]
public class CalculatorController : ControllerBase
{
    private readonly IMediator _mediator;

    public CalculatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("calculate-ads")]
    public async Task<ActionResult<decimal>> CalculateAverageDailySales(
        [FromBody] CalculateAverageDailySalesModel model)
    {
        var request = new CalculateAverageDailySalesOperation.Request(model.ProductId);
        var response = await _mediator.Send(request, CancellationToken);

        return Ok(response.AverageDailySales);
    }
    
    [HttpPost]
    [Route("calculate-sales-prediction")]
    public async Task<ActionResult<decimal>> CalculateSalesPrediction(
        [FromBody] CalculateSalesPredictionModel model)
    {
        var request = new CalculateSalesPredictionOperation.Request(
            model.ProductId,
            model.Days);
        var response = await _mediator.Send(request, CancellationToken);

        return Ok(response.SalesPrediction);
    }
    
    [HttpPost]
    [Route("calculate-demand-supplied")]
    public async Task<ActionResult<decimal>> CalculateDemandSupplied(
        [FromBody] CalculateDemandSuppliedModel model)
    {
        var request = new CalculateDemandSuppliedOperation.Request(model.ProductId, model.Days, model.Date);
        var response = await _mediator.Send(request, CancellationToken);
    
        return Ok(response.Demand);
    }
    
    [HttpPost]
    [Route("calculate-demand")]
    public async Task<ActionResult<decimal>> CalculateDemand(
        [FromBody] CalculateDemandModel model)
    {
        var request = new CalculateDemandOperation.Request(model.ProductId, model.Days);
        var response = await _mediator.Send(request, CancellationToken);
    
        return Ok(response.Demand);
    }
}