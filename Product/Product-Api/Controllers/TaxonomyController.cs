using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.DTOs;
using ProductApplication.Queries;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxonomyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaxonomyController(IMediator mediator) => _mediator = mediator;

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryNodeDto>>> GetCategoryTree()
            => Ok(await _mediator.Send(new GetCategoryTreeQuery()));

        // [HttpGet("brands")]
        // public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        //     => Ok(await _mediator.Send(new GetBrandsQuery())); // Assuming GetBrandsQuery implemented similarly
    }
}
