using Domain.Entities;

namespace Application.Authentication.common
{
    public record AuthenticationResult(User User, string Token, bool Success, string Message, bool Verified, int StatusCode);
}