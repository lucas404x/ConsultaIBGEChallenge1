using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Core.Authentication;
using ConsultaIbge.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConsultaIbge.Application.Services;

public sealed class TokenService : ITokenService
{
    public string Generate(User user)
    {
        var handler = new JwtSecurityTokenHandler();
        var credentials = new SigningCredentials(new SymmetricSecurityKey(JwtHelper.PrivateKeyBytes), SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Name));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        return ci;
    }
}
