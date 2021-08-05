
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Api.Data.Contracts;
using Api.Entities;
using Api.Entities.Users;
using Api.Services;
using Api.WebApi.Models;
using Api.WebFramework.Api;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.WebApi.Controllers.V2
{
    [ApiVersion("2")]
    public class Users : V1.Users
    {
        public Users(IUserRepository userRepository, IJwtService jwtService, ILogger<V1.Users> logger, UserManager<User> userManager, RoleManager<Role> roleManager)
            : base(userRepository, jwtService, logger, userManager, roleManager)
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

        public override Task<List<User>> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }

        public override Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }

        public override Task<AccessToken> Token(string userName, string password, CancellationToken cancellationToken)
        {
            return base.Token(userName, password, cancellationToken);
        }

        public override Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            return base.Update(id, user, cancellationToken);
        }
    }
}
