using Drug_Procurement.CQRS.Commands.Create;
using Drug_Procurement.CQRS.Commands.Update;
using Drug_Procurement.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drug_Procurement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory(CreateInventoryCommand command)
        {
            await _mediator.Send(command);
            return Ok("Product Created Successfully");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllInventory()
        {
            return Ok(await _mediator.Send(new GetAllInventoryQuery()));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryById(UpdateInventoryCommand command, int id)
        {
            if(command.Id != id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventoryById(int id)
        {
            var inventory = await _mediator.Send(new GetInventoryByIdQuery { Id = id});
            if (inventory == null)
                return NotFound();
            return Ok(inventory);
        }
    }
}
