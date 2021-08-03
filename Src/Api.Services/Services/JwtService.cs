
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Api.Common;
using Api.Entities;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
    public class JwtService : IJwtService
    {
        private SiteSettings SiteSettings { get; }

        public JwtService(IOptionsSnapshot<SiteSettings> siteSettings)
        {
            SiteSettings = siteSettings.Value;
        }
        public string Generate(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(SiteSettings.JwtSettings.SecretKey);// longer that 16 character
            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var claims = GetClaims(user);
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = SiteSettings.JwtSettings.Issuer,
                Audience = SiteSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(SiteSettings.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(SiteSettings.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler handler = new();
            SecurityToken securityToken = handler.CreateToken(descriptor);
            var Jwt = handler.WriteToken(securityToken);
            return Jwt;
        }
        private static IEnumerable<Claim> GetClaims(User user)
        {

            List<Claim> claims = new()
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            return claims;
        }

    }
}
