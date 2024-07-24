using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Users.Request.Commands;
using Application.DTOs.UserDto;

namespace API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var request = new RegisterUserRequest { RegisterUserDto = registerUserDto };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        // [HttpPost("login")]
        // public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        // {
        //     var request = new LoginRequest { LoginDto = loginDto };
        //     var response = await _mediator.Send(request);
        //     return Ok(response);
        // }


        // [HttpGet("profile")]
        // public async Task<IActionResult> Profile()
        // {
        //     var response = await _mediator.Send(new GetUserProfileRequest());
        //     return Ok(response);
        // }

        // [HttpPut("update-profile")]
        // public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequest request)
        // {
        //     var response = await _mediator.Send(request);
        //     return Ok(response);
        // }

        // [HttpDelete("delete-profile")]
        // public async Task<IActionResult> DeleteProfile()
        // {
        //     var response = await _mediator.Send(new DeleteUserProfileRequest());
        //     return Ok(response);
        // }
    }
}