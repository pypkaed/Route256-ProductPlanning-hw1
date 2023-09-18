using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.MediatROperations.Sales;
using ProductPlanningApplication.Dtos;
using ProductPlanningPresentation.Models;

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
    
    public CancellationToken CancellationToken 
        => HttpContext.RequestAborted;

    [HttpPost]
    [Route("/CreateSale")]
    public async Task<ActionResult<SaleDto>> CreateSale(
        [FromBody] CreateSaleModel model)
    {
        var request = new CreateSaleOperation.Request(
            model.ProductId,
            model.Date,
            model.Sales,
            model.Stock);
        var response = await _mediator.Send(request, CancellationToken);

        return Ok(response.Sale);
    }
    
    [HttpPost]
    [Route("/CreateSeasonalCoefficient")]
    public async Task<ActionResult<SeasonalCoefficientDto>> CreateSeasonalCoefficient(
        [FromBody] CreateSeasonalCoefficientModel model)
    {
        var request = new CreateSeasonalCoefficientOperation.Request(
            model.ProductId,
            model.Coefficient,
            model.Month);
        var response = await _mediator.Send(request, CancellationToken);

        return Ok(response.SeasonalCoefficient);
    }
}