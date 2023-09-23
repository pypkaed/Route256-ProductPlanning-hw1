using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.Dtos;
using ProductPlanningPresentation.Requests;

namespace ProductPlanningPresentation.Controllers;

[ApiController]
[Route("api/")]
public class CreateController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("create-sale")]
    public async Task<ActionResult<SaleDto>> CreateSale(
        [FromBody] CreateSaleUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CreateSaleRequest(
            userRequest.ProductId,
            userRequest.Date,
            userRequest.Sales,
            userRequest.Stock);
        var response = await _mediator.Send(sendRequest, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost]
    [Route("create-seasonal-coefficient")]
    public async Task<ActionResult<SeasonalCoefficientDto>> CreateSeasonalCoefficient(
        [FromBody] CreateSeasonalCoefficientUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CreateSeasonalCoefficientRequest(
            userRequest.ProductId,
            userRequest.Coefficient,
            userRequest.Month);
        var response = await _mediator.Send(sendRequest, cancellationToken);

        return Ok(response);
    }
}