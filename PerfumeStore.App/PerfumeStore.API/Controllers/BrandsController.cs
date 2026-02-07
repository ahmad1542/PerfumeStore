using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.Brands.Commands.CreateBrand;
using PerfumeStore.Application.Brands.Commands.UpdateBrand;
using PerfumeStore.Application.Brands.Dtos;
using PerfumeStore.Application.Brands.Queries.GetAllBrands;
using PerfumeStore.Application.Brands.Queries.GetBrandById;

namespace PerfumeStore.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto?>> GetById([FromRoute] int id) {
            var brand = await mediator.Send(new GetBrandByIdQuery(id));
            
            return Ok(brand);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAll([FromQuery] string? search) {
            var brands = await mediator.Send(new GetAllBrandsQuery(search));
            return Ok(brands);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandCommand command) {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBrand([FromRoute] int id, [FromBody] UpdateBrandCommand command) {
            command.ID = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
