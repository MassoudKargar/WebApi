using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Api.Data.Contracts;
using Api.Entities;
using Api.WebApi.Models;
using Api.WebFramework.Api;
using Api.WebFramework.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [ApiResultFilter]
    public class UserController : ControllerBase
    {
        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        private IUserRepository UserRepository { get; }

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

        [HttpPost]
        public async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            var exists = await UserRepository.TableNoTracking.AnyAsync(a => a.UserName == userDto.UserName, cancellationToken);
            if (exists) return BadRequest();
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
