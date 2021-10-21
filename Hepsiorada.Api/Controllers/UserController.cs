using Hepsiorada.Application.Commands.Product;
using Hepsiorada.Application.Commands.User;
using Hepsiorada.Application.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            GetUsersCommand getUsersCommand = new GetUsersCommand();
            List<UserDTO> users = await _mediator.Send(getUsersCommand);

            return Ok(users);//TODO
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDTO)
        {
            CreateUserCommand createCommand = userDTO.Adapt<CreateUserCommand>();
            await _mediator.Send(createCommand);

            return Ok();//TODO
        }
    }
}
