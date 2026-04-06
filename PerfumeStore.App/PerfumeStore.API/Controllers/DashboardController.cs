using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.Dashboard.Dtos;
using PerfumeStore.Application.Dashboard.Queries.GetDashboard;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController(IMediator mediator) : ControllerBase {
        [HttpGet]
        public async Task<ActionResult<DashboardDto>> Get() {
            var dashboard = await mediator.Send(new GetDashboardQuery());
            return Ok(dashboard);
        }
    }
}
