using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.MediatROperations.Sales;
using ProductPlanningPresentation.Models;
using ProductPlanningPresentation.Responses;


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
    public async Task<ActionResult<CalculateAverageDailySalesResponse>> CalculateAverageDailySales(
        [FromBody] CalculateAverageDailySalesRequest request)
    {
        var sendRequest = new CalculateAverageDailySalesOperation.Request(request.ProductId);
        var response = await _mediator.Send(sendRequest, CancellationToken);

        return Ok(new CalculateAverageDailySalesResponse(response.AverageDailySales));
    }
    
    [HttpPost]
    [Route("calculate-sales-prediction")]
    public async Task<ActionResult<CalculateSalesPredictionResponse>> CalculateSalesPrediction(
        [FromBody] CalculateSalesPredictionRequest request)
    {
        var sendRequest = new CalculateSalesPredictionOperation.Request(
            request.ProductId,
            request.Days);
        var response = await _mediator.Send(sendRequest, CancellationToken);

        return Ok(new CalculateSalesPredictionResponse(response.SalesPrediction));
    }
    
    [HttpPost]
    [Route("calculate-demand-supplied")]
    public async Task<ActionResult<CalculateDemandSuppliedResponse>> CalculateDemandSupplied(
        [FromBody] CalculateDemandSuppliedRequest request)
    {
        var sendRequest = new CalculateDemandSuppliedOperation.Request(request.ProductId, request.Days, request.Date);
        var response = await _mediator.Send(sendRequest, CancellationToken);
    
        return Ok(new CalculateDemandSuppliedResponse(response.Demand));
    }
    
    [HttpPost]
    [Route("calculate-demand")]
    public async Task<ActionResult<CalculateDemandResponse>> CalculateDemand(
        [FromBody] CalculateDemandRequest request)
    {
        var sendRequest = new CalculateDemandOperation.Request(request.ProductId, request.Days);
        var response = await _mediator.Send(sendRequest, CancellationToken);
    
        return Ok(new CalculateDemandResponse(response.Demand));
    }
}