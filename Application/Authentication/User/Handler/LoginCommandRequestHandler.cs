// using Application.Authentication.common;
using Application.Authentication.common;
using Application.Authentication.Request;
using Application.Persistence.Contracts;
using Application.Persistence.Contracts.Auth;
using Application.Response;
using MediatR;

namespace Application.Authentication.User.Handler
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
            bool profileExist = false;

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
            else if ((await _otpRepository.FindUser(user.Id)) != null)
            {
                response.Success = true;
                response.Message = "user not verified";
                response.StatusCode = 400;
            }
            else
            {
                response.Success = true;
                response.Message = "User logged in successfully";
                response.StatusCode = 200;
                var companyImage = "";
                var companyName = "";
                if (user.User_Type.ToLower() == "investor")
                {
                    var profile = await _investorProfileRepository.GetInvestorProfile(user.Id);
                    if (profile != null)
                    {
                        companyImage = profile.Profile_Image;
                        companyName = profile.Full_Name;
                        profileExist = true;
                    }

                }
                else if (user.User_Type.ToLower() == "startup")
                {
                    var profile = await _startupProfileRepository.StartupProfileExists(user.Id);
                    if (profile != null)
                    {
                        companyImage = profile.ImageLogo;
                        companyName = profile.StartupName;
                        profileExist = true;
                    }
                }
                token = _jwtTokenGenerator.GenerateToken(user, false, companyImage, companyName, profileExist);
                flag = true;
            }

            return new AuthenticationResult(user, token, response.Success, response.Message, flag, response.StatusCode);
        }
    }
}
