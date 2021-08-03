using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Api.Common.Exceptions;
using Api.Common.Utilities;
using Api.Data.Contracts;
using Api.Entities;
using Api.Services;
using Api.WebApi.Models;
using Api.WebFramework.Api;
using Api.WebFramework.Filters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [ApiResultFilter]
    [Authorize]
    public class UserController : ControllerBase
    {
        public UserController(IUserRepository userRepository, IJwtService jwtService)
        {
            UserRepository = userRepository;
            JwtService = jwtService;
        }

        private IUserRepository UserRepository { get; }
        private IJwtService JwtService { get; }

        [HttpGet]
        public async Task<List<User>> Get(CancellationToken cancellationToken)
        {
            var users = await UserRepository.TableNoTracking.ToListAsync(cancellationToken);
            return users;
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            var user = await UserRepository.GetByIdAsync(cancellationToken, id);
            if (user is null) return NotFound();
            return user;
        }
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<string> Token(string userName, string password, CancellationToken cancellationToken)
        {
            var user = await UserRepository.GetByUserAndPass(userName, password, cancellationToken);
            if (user is null) throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");
            return JwtService.Generate(user);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
           var userName = HttpContext.User.Identity.GetUserName();
           var userId = HttpContext.User.Identity.GetUserId<int>();

            User user = new()
            {
                UserName = userDto.UserName,
                FullName = userDto.FullName,
                Age = userDto.Age,
                Gender = userDto.Gender,
            };
            await UserRepository.AddAsync(user, userDto.Password, cancellationToken);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            var updateUser = await UserRepository.GetByIdAsync(cancellationToken, id);
            updateUser.UserName = user.UserName;
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.FullName = user.FullName;
            updateUser.Age = user.Age;
            updateUser.Gender = user.Gender;
            updateUser.IsActive = user.IsActive;
            updateUser.LastLoginDate = user.LastLoginDate;
            await UserRepository.UpdateAsync(updateUser, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await UserRepository.GetByIdAsync(cancellationToken, id);
            await UserRepository.DeleteAsync(user, cancellationToken);
            return Ok();
        }
    }
}
