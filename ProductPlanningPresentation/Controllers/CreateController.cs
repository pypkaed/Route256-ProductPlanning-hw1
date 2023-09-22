using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.MediatROperations.Sales;
using ProductPlanningApplication.Dtos;
using ProductPlanningPresentation.Models;

namespace ProductPlanningPresentation.Controllers;

[ApiController]
[Route("api")]
public class CreateController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("/create-sale")]
    public async Task<ActionResult<SaleDto>> CreateSale(
        [FromBody] CreateSaleRequest request)
    {
        var sendRequest = new CreateSaleOperation.Request(
            request.ProductId,
            request.Date,
            request.Sales,
            request.Stock);
        var response = await _mediator.Send(sendRequest, CancellationToken);

        return Ok(response.Sale);
    }
    
    [HttpPost]
    [Route("/create-seasonal-coefficient")]
    public async Task<ActionResult<SeasonalCoefficientDto>> CreateSeasonalCoefficient(
        [FromBody] CreateSeasonalCoefficientRequest request)
    {
        var sendRequest = new CreateSeasonalCoefficientOperation.Request(
            request.ProductId,
            request.Coefficient,
            request.Month);
        var response = await _mediator.Send(sendRequest, CancellationToken);

        return Ok(response.SeasonalCoefficient);
    }
}