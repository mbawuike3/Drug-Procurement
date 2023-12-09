using Drug_Procurement.CQRS.Commands.Create;
using Drug_Procurement.CQRS.Commands.Delete;
using Drug_Procurement.CQRS.Commands.Update;
using Drug_Procurement.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drug_Procurement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(UpdateRoleCommand command, int id)
        {
            if (id != command.Id) return NotFound();
            return Ok(await _mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllRolesQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _mediator.Send(new GetRolesByIdQuery { Id = id });
            return Ok(role);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(DeleteRoleCommand query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
