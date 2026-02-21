using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.Inventory.Commands.CreateInventory;
using PerfumeStore.Application.Inventory.Queries.GetAllInventory;
using PerfumeStore.Application.Inventory.Queries.GetInventoryById;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventory([FromRoute] int id) {
            var inventory = mediator.Send(new GetInventoryByIdQuery(id));
            return Ok(inventory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? search) {
            var inventories = mediator.Send(new GetAllInventoryQuery(search));
            return Ok(inventories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryCommand command) {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetInventory), new { id }, null);
        }
    }
}
