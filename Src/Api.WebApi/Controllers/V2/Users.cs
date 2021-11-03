
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Api.Data.Contracts;
using Api.Entities;
using Api.Entities.Users;
using Api.Services;
using Api.Services.Jwt;
using Api.WebApi.Models;
using Api.WebFramework.Api;

using Ccms.Common.WebSetting;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.WebApi.Controllers.V2
{
    [ApiVersion("2")]
    public class Users : V1.Users
    {
        public Users(
            IUserRepository userRepository, 
            ILogger<V1.Users> logger, 
            IJwtInteface jwtService,
            UserManager<User> userManager,
            RoleManager<Role> roleManager, 
            SignInManager<User> signInManager) 
            : base(userRepository,
                  logger,
                  jwtService,
                  userManager,
                  roleManager,
                  signInManager)
        {
        }

        public override Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            return base.Create(userDto, cancellationToken);
        }

        public override Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }

        public override Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }

        public override Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }

        public override Task<ActionResult> Token([FromForm] TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            return base.Token(tokenRequest, cancellationToken);
        }

        public override Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            return base.Update(id, user, cancellationToken);
        }
    }
}
