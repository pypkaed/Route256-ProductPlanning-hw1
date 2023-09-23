using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.Operations.Requests;
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
        [FromBody] CalculateAverageDailySalesUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CalculateAverageDailySalesRequest(userRequest.ProductId);
        var response = await _mediator.Send(sendRequest, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost]
    [Route("calculate-sales-prediction")]
    public async Task<ActionResult<CalculateSalesPredictionDto>> CalculateSalesPrediction(
        [FromBody] CalculateSalesPredictionUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CalculateSalesPredictionRequest(
            userRequest.ProductId,
            userRequest.Days);
        var response = await _mediator.Send(sendRequest, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost]
    [Route("calculate-demand-supplied")]
    public async Task<ActionResult<CalculateDemandSuppliedDto>> CalculateDemandSupplied(
        [FromBody] CalculateDemandSuppliedUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CalculateDemandSuppliedRequest(userRequest.ProductId, userRequest.Days, userRequest.Date);
        var response = await _mediator.Send(sendRequest, cancellationToken);
    
        return Ok(response);
    }
    
    [HttpPost]
    [Route("calculate-demand")]
    public async Task<ActionResult<CalculateDemandDto>> CalculateDemand(
        [FromBody] CalculateDemandUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CalculateDemandRequest(userRequest.ProductId, userRequest.Days);
        var response = await _mediator.Send(sendRequest, cancellationToken);
    
        return Ok(response);
    }
}