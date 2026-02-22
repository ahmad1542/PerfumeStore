using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.Inventory.Commands.CreateInventory;
using PerfumeStore.Application.Inventory.Commands.UpdateInventory;
using PerfumeStore.Application.Inventory.Queries.GetAllInventory;
using PerfumeStore.Application.Inventory.Queries.GetInventoryById;
using PerfumeStore.Application.Persons.Commands.UpdatePerson;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventory([FromRoute] int id) {
            var inventory = await mediator.Send(new GetInventoryByIdQuery(id));
            return Ok(inventory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? search) {
            var inventories = await mediator.Send(new GetAllInventoryQuery(search));
            return Ok(inventories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryCommand command) {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetInventory), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateInventory([FromRoute] int id, [FromBody] UpdateInventoryCommand command) {
            command.ProductID = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
