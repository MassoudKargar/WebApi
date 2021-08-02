﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Api.Data.Contracts;
using Api.Entities;
using Api.WebFramework.Api;
using Api.WebFramework.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        private IUserRepository UserRepository { get; }

        [HttpGet]
        [ApiResultFilter]
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
        public async Task<ApiResult<User>> Create(User user, CancellationToken cancellationToken)
        {
            await UserRepository.AddAsync(user, cancellationToken);
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
