using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.ProductCategories.Commands.CreateProductCategory;
using PerfumeStore.Application.ProductCategories.Commands.UpdateProductCategory;
using PerfumeStore.Application.ProductCategories.Dtos;
using PerfumeStore.Application.ProductCategories.Queries.GetAllProductCategories;
using PerfumeStore.Application.ProductCategories.Queries.GetProductCategoryById;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto?>> GetById([FromRoute] int id) {
            var productCategory = await mediator.Send(new GetProductCategoryByIdQuery(id));

            return Ok(productCategory);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAll([FromQuery] string? search) {
            var productCategories = await mediator.Send(new GetAllProductCategoriesQuery(search));
            return Ok(productCategories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateProductCategoryCommand command) {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBrand([FromRoute] int id, [FromBody] UpdateProductCategoryCommand command) {
            command.ID = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
