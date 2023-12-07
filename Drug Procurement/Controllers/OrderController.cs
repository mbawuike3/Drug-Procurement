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
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllOrderQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery { Id = id });
            if (order == null) return NotFound();
            return Ok(order);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand command, int id)
        {
            if (id != command.Id) return NotFound();
            return Ok(await _mediator.Send(command));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(await _mediator.Send(new DeleteOrderCommand { Id = id }));
        }
    }
}
