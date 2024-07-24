using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.UserDto;
using Application.Response;
using MediatR;

namespace Application.Features.Users.Request.Commands
{
    public class RegisterUserRequest : IRequest<BaseResponse>
    {
        public RegisterUserDto RegisterUserDto { get; set; }

    }
}