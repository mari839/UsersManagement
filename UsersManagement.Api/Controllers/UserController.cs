﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.Features.Users.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersManagement.Api.Controllers
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

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<CreateUserDto>> CreateUser(CreateUserCommand createUserCommand)
        {
            var res = await _mediator.Send(createUserCommand);
            return Ok(res);
        }

    }
}
