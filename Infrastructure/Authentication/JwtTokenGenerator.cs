using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Application.Persistence.Contracts.Common;
using Application.Persistence.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.services;

namespace Infrastructure.Authentication;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    public readonly DateTimeProvider _dateTimeProvider;
    public readonly Jwtsettings _jwtsettings;
    public JwtTokenGenerator(IOptions<Jwtsettings> jwtsettings, DateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtsettings = jwtsettings.Value;

    }
    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))), SecurityAlgorithms.HmacSha256Signature);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)

        };

        var securityToken = new JwtSecurityToken(
                            issuer: Environment.GetEnvironmentVariable("Issuer"),
                            audience: Environment.GetEnvironmentVariable("Audience"),
                            claims: claims,
                            expires: _dateTimeProvider.CreateTime.AddMinutes(int.Parse(Environment.GetEnvironmentVariable("Expiry_In_Minutes"))),
                            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

}