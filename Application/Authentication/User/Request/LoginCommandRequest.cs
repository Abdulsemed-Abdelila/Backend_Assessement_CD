using Application.Authentication.common;
using MediatR;

namespace Application.Authentication.Request
{
    public record LoginCommandRequest(string Email, string Password) : IRequest<AuthenticationResult>;
}