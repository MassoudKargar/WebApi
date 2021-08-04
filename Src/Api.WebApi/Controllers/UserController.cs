using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Api.Common;
using Api.Common.Exceptions;
using Api.Data.Contracts;
using Api.Entities;
using Api.Entities.Users;
using Api.Services;
using Api.WebApi.Models;
using Api.WebFramework.Api;
using Api.WebFramework.Filters;

using AutoMapper;

using ElmahCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [ApiResultFilter]
    public class UserController : ControllerBase
    {
        public UserController(
            IUserRepository userRepository,
            IJwtService jwtService,
            ILogger<UserController> logger,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            Mapper mapper,
            SignInManager<User> signInManager)
        {
            UserRepository = userRepository;
            JwtService = jwtService;
            Logger = logger;
            UserManager = userManager;
            RoleManager = roleManager;
            this.Mapper = mapper;
            SignInManager = signInManager;
        }

        private IUserRepository UserRepository { get; }
        private IJwtService JwtService { get; }
        private ILogger<UserController> Logger { get; }
        private readonly UserManager<User> UserManager;
        private readonly RoleManager<Role> RoleManager;
        private readonly Mapper Mapper;
        private readonly SignInManager<User> SignInManager;

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
        [HttpGet("/[action]")]
        [AllowAnonymous]
        public async Task<AccessToken> Token(string userName, string password, CancellationToken cancellationToken)
        {

            var user = await UserRepository.GetByUserAndPass(userName, password, cancellationToken);
            if (user is null) throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");
            //await UserRepository.UpdateSecurityStampAsync(user, cancellationToken);
            return await JwtService.GenerateAsync(user);
        }
        [HttpPost]
        [AllowAnonymous]
        public virtual async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        { 
            Logger.LogError("متد Create فراخوانی شد");
            HttpContext.RiseError(new Exception("متد Create فراخوانی شد"));

            //var exists = await userRepository.TableNoTracking.AnyAsync(p => p.UserName == userDto.UserName);
            //if (exists)
            //    return BadRequest("نام کاربری تکراری است");
            var user2 = Mapper.Map<User>(userDto);
            var user = new User
            {
                Age = userDto.Age,
                FullName = userDto.FullName,
                Gender = userDto.Gender,
                UserName = userDto.UserName,
                Email = userDto.Email
            };
            try
            {
                var result = await UserManager.CreateAsync(user, userDto.Password);
                var result2 = await RoleManager.CreateAsync(new Role
                {
                    Name = "Admin",
                    Description = "admin role"
                });
                var result3 = await UserManager.AddToRoleAsync(user, "Admin");

            }
            catch (AppException e)
            {
                throw new AppException(ApiResultStatusCode.NotFound, "مقادیر ورودی صحیح نم یباشند", HttpStatusCode.NotFound, e, null);
            }

            //await userRepository.AddAsync(user, userDto.Password, cancellationToken);
            return user;
        }

        [HttpPut]
        public virtual async Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
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


        [HttpDelete("{id:int}")]
        public virtual async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await UserRepository.GetByIdAsync(cancellationToken, id);
            await UserRepository.DeleteAsync(user, cancellationToken);

            return Ok();
        }

    }
}
