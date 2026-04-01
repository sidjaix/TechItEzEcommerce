using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.Queries;
using ProductApplication.DTOs;
using ProductApplication.Command;
using Microsoft.AspNetCore.Authorization;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Mandate a valid JWT for the entire controller
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;
    public CatalogController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CatalogItemDto>>> GetActiveItems()
        => Ok(await _mediator.Send(new GetActiveCatalogItemsQuery()));

    [HttpGet("{slug}")]
    public async Task<ActionResult<CatalogItemDetailDto>> GetBySlug(string slug)
    {
        var result = await _mediator.Send(new GetCatalogItemBySlugQuery(slug));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CatalogItemDto>>> Search([FromQuery] string term)
        => Ok(await _mediator.Send(new SearchCatalogItemsQuery(term)));

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateCatalogItemCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPost("variant")]
    public async Task<IActionResult> AddVariant(AddVariantCommand command)
    {
        var result = await _mediator.Send(command);
        return result ? Ok() : BadRequest("Could not add variant.");
    }

    [HttpPost("review")]
    public async Task<IActionResult> AddReview(AddProductReviewCommand command)
    {
        var result = await _mediator.Send(command);
        return result ? Ok() : BadRequest();
    }

    [HttpPut("semantic-profile")]
    public async Task<IActionResult> UpdateSemanticProfile(UpdateSemanticProfileCommand command)
    {
        var result = await _mediator.Send(command);
        return result ? Ok() : NotFound();
    }
}