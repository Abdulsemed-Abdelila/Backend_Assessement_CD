using Application.Features.Users.Request.Commands;
using Application.Persistence.Contracts;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Handler.Commands
{
    public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, BaseResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public RegisterUserRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<BaseResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}