using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.MediatROperations.Sales;
using ProductPlanningApplication.Dtos;
using ProductPlanningPresentation.Requests;


namespace ProductPlanningPresentation.Controllers;

[ApiController]
[Route("api/")]
public class CalculatorController : ControllerBase
{
    private readonly IMediator _mediator;

    public CalculatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("calculate-ads")]
    public async Task<ActionResult<CalculateAverageDailySalesDto>> CalculateAverageDailySales(
        [FromBody] CalculateAverageDailySalesRequest request,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CalculateAverageDailySalesOperation.Request(request.ProductId);
        var response = await _mediator.Send(sendRequest, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost]
    [Route("calculate-sales-prediction")]
    public async Task<ActionResult<CalculateSalesPredictionDto>> CalculateSalesPrediction(
        [FromBody] CalculateSalesPredictionRequest request,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CalculateSalesPredictionOperation.Request(
            request.ProductId,
            request.Days);
        var response = await _mediator.Send(sendRequest, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost]
    [Route("calculate-demand-supplied")]
    public async Task<ActionResult<CalculateDemandSuppliedDto>> CalculateDemandSupplied(
        [FromBody] CalculateDemandSuppliedRequest request,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CalculateDemandSuppliedOperation.Request(request.ProductId, request.Days, request.Date);
        var response = await _mediator.Send(sendRequest, cancellationToken);
    
        return Ok(response);
    }
    
    [HttpPost]
    [Route("calculate-demand")]
    public async Task<ActionResult<CalculateDemandDto>> CalculateDemand(
        [FromBody] CalculateDemandRequest request,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CalculateDemandOperation.Request(request.ProductId, request.Days);
        var response = await _mediator.Send(sendRequest, cancellationToken);
    
        return Ok(response);
    }
}