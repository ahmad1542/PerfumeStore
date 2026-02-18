using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.Persons.Commands.CreatePerson;
using PerfumeStore.Application.Persons.Commands.UpdatePerson;
using PerfumeStore.Application.Persons.Dtos;
using PerfumeStore.Application.Persons.Queries.GetAllPersons;
using PerfumeStore.Application.Persons.Queries.GetPersonByPhoneNo;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController(IMediator mediator) : ControllerBase {

        [HttpGet("{phoneNo}")]
        public async Task<ActionResult<PersonDto?>> GetByPhoneNo([FromRoute] string phoneNo) {
            var person = await mediator.Send(new GetPersonByPhoneNoQuery(phoneNo));

            return Ok(person);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll([FromQuery] string? search) {
            var persons = await mediator.Send(new GetAllPersonsQuery(search));
            return Ok(persons);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand command) {
            string phoneNo = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByPhoneNo), new { phoneNo }, null);
        }

        [HttpPatch("{phoneNo}")]
        public async Task<IActionResult> UpdatePerson([FromRoute] string phoneNo, [FromBody] UpdatePersonCommand command) {
            command.Phone = phoneNo;
            await mediator.Send(command);
            return NoContent();
        }

    }
}
