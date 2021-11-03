namespace Api.Services.Jwt
{
    using Api.Common;
    using Api.Entities;
    using Api.Entities.Users;

    using Ccms.Common;
    using Dapper;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using System;
    using System.Collections.Generic;
using System.Data;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtService : IJwtInteface, IScopedDependency
    {
        private SiteSettings SiteSetting { get; }

        public JwtService(IOptionsSnapshot<SiteSettings> settings)
        {
            SiteSetting = settings.Value;
        }

        /// <summary>
        /// این متود برای ساخت توکن و بازگرداندن توکن به کاربر میباشد
        /// </summary>
        /// <param name="user">اطلاعات کاربر را گرفته و برای ساخت توکن استفاده میکند</param>
        /// <returns> باز میگرداند AccessToken توکن در قالب </returns>
        public AccessToken Generate(User user, Role role)
       {
            var secretKey = Encoding.UTF8.GetBytes(SiteSetting.JwtSettings.SecretKey); // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(SiteSetting.JwtSettings.EncryptKey); //must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            //var claims = await GetClaimsAsync(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = SiteSetting.JwtSettings.Issuer,
                Audience = SiteSetting.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(SiteSetting.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(SiteSetting.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(GetClaims(user, role))
            };

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            string encryptedJwt = tokenHandler.WriteToken(securityToken);
            var token = new AccessToken(securityToken);
            return token;
        }
        /// <summary>
        /// این متود برای آپدیت کردن توکن و بازگرداندن به کاربر میباشد
        /// </summary>
        /// <param name="claims">اطلاعات داخل توکن کاربر را گرفته و برای ساخت توکن مجدد استفاده میکند</param>
        /// <returns> باز میگرداند AccessToken توکن در قالب </returns>
        public AccessToken Generate(Dictionary<string, string> claims)
        {
            var secretKey = Encoding.UTF8.GetBytes(SiteSetting.JwtSettings.SecretKey); // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(SiteSetting.JwtSettings.EncryptKey); //must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            //var claims = await GetClaimsAsync(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = SiteSetting.JwtSettings.Issuer,
                Audience = SiteSetting.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(SiteSetting.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(SiteSetting.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(GetClaims(claims))
            };

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            string encryptedJwt = tokenHandler.WriteToken(securityToken);
            var token = new AccessToken(securityToken);
            return token;
        }
        /// <summary>
        /// ثبت دسترسی های کاربر در توکن
        /// </summary>
        /// <param name="user">دریافت اطلاعات کاربر</param>
        /// <param name="role">دریافت اطلاعات دسترسی کاربر</param>
        /// <returns>Task<IEnumerable<Claim>></returns>
        private static IEnumerable<Claim> GetClaims(User user, Role role)
        {
            //add custom claims
            List<Claim> list = new()
            {
                new("FullName", user.FullName),
                new("PersonId", user.UserName.ToString()),
                new("UserId", user.Id.ToString()),
                new("RoleName", role.Name),
                new("RoleCaption", role.Description),
                new("RoleId", role.Name.ToString()),
            };
            return list;
        }
        
        /// <summary>
        /// ثبت دسترسی های کاربر در توکن
        /// </summary>
        /// <param name="claims">دریافت اطلاعات کاربر</param>
        /// <returns>Task<IEnumerable<Claim>></returns>
        private static IEnumerable<Claim> GetClaims(Dictionary<string, string> claims)
        {
            //add custom claims
            List<Claim> list = new();
            foreach (var claim in claims)
            {
                if (claim.Value is not null)
                {
                    list.Add(new Claim(claim.Key, claim.Value));
                }
            }
            return list;
        }

        /// <summary>
        /// رمز گشایی اطلاعات توکن
        /// </summary>
        /// <param name="token"> توکن </param>
        /// <returns></returns>
        public Dictionary<string, string> Read(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SiteSetting.JwtSettings.SecretKey)),
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SiteSetting.JwtSettings.EncryptKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            handler.ValidateToken(token, validations, out SecurityToken tokenSecure);

            var tokenData = tokenSecure.ToString().Split("}.{")[1].TrimEnd('}').Replace('"', ' ');
            var list = tokenData.Split(',').AsList();
            string[,] vs = new string[list.Count, 2];
            Dictionary<string, string> valuePairs = new();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    vs[i, j] = list[i].Split(':')[j].Trim();
                }
                valuePairs.Add(vs[i, 0], vs[i, 1]);
            }
            return valuePairs;
        }

    
    }
}
