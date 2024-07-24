using Application.Authentication.common;
using Application.Authentication.Users.Request;
using Application.Persistence.Contracts;
using Application.Persistence.Contracts.Auth;
using Application.Response;
using MediatR;

namespace Application.Authentication.Users.Handler
{
    public sealed class LoginCommandRequestHandler : IRequestHandler<LoginCommandRequest, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;

        public LoginCommandRequestHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IPasswordService passwordService,
            IUserRepository userRepository
            )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<AuthenticationResult> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var user = await _userRepository.GetByEmail(request.Email);
            var token = "";
            bool flag = false;

            if (user == null)
            {
                response.Success = false;
                response.Message = "User email or password is incorrect";
                response.StatusCode = 400;
            }


            else if (!_passwordService.VerifyPassword(request.Password, user.Password))
            {
                response.Success = false;
                response.Message = "User email or password is incorrect";
                response.StatusCode = 400;
            }

            else
            {
                response.Success = true;
                response.Message = "User logged in successfully";
                response.StatusCode = 200;
                token = _jwtTokenGenerator.GenerateToken(user);
                flag = true;
            }

            return new AuthenticationResult(user, token, response.Success, response.Message, flag, response.StatusCode);
        }
    }
}
