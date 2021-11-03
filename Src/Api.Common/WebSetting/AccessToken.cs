namespace Ccms.Common
{
    using System.IdentityModel.Tokens.Jwt;

    public class AccessToken
    {
        public AccessToken()
        {

        }
        public string Token { get; set; }
        public double? ExpiresIn { get; set; }

        public AccessToken(JwtSecurityToken securityToken) : this()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            ExpiresIn = (securityToken.ValidTo - securityToken.IssuedAt).TotalMinutes;
        }
    }
    public class AccessTokenModel
    {
        public AccessTokenModel()
        {

        }
        public AccessTokenModel(string token) : this()
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
