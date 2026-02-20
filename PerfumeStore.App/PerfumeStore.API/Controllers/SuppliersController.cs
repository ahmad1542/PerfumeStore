using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.Suppliers.Commands.CreateSupplier;
using PerfumeStore.Application.Suppliers.Commands.UpdateSupplier;
using PerfumeStore.Application.Suppliers.Dtos;
using PerfumeStore.Application.Suppliers.Queries.GetAllSuppliers;
using PerfumeStore.Application.Suppliers.Queries.GetSupplierById;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto?>> GetById([FromRoute] Guid id) {
            var supplier = await mediator.Send(new GetSupplierByIdQuery(id));
            return Ok(supplier);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll([FromQuery] string? search) {
            var suppliers = await mediator.Send(new GetAllSuppliersQuery(search));
            return Ok(suppliers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierCommand command) {
            Guid id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSupplier([FromRoute] Guid id, [FromBody] UpdateSupplierCommand command) {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
