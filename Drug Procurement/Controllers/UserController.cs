﻿ using Drug_Procurement.CQRS.Commands.Create;
using Drug_Procurement.CQRS.Commands.Update;
using Drug_Procurement.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drug_Procurement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("User created Successfully");
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command, int id)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return Ok("User Credentials Updated");
        }
        [HttpPost("User-Login")]
        public async Task<IActionResult> LoginUser(LoginUserCommand command)
        {
            var token = await _mediator.Send(command);
            if (token == null)
            {
                return BadRequest(token);
            }
            return Ok(token);
        }
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> UserPasswordReset(ResetUserPasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
