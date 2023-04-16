using Onion.JwtApp.Application.Features.CQRS;
using Onion.JwtApp.API.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Onion.JwtApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterAppUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(CheckAppUserQueryRequest request)
        {
            var dto = await _mediator.Send(request);
            if(dto.IsExist)
            {
                return Created("", JwtGenerator.GenerateToken(dto));
            }
            else
            {
                return BadRequest("Username or Password is incorrect!");
            }
        }
    }
}
