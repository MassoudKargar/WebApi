﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Api.Common.Exceptions;
using Api.Data.Contracts;
using Api.Entities;
using Api.Entities.Users;
using Api.Services;
using Api.Services.Jwt;
using Api.WebApi.Models;
using Api.WebFramework.Api;

using Ccms.Common.WebSetting;

using ElmahCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.WebApi.Controllers.V1
{
    [ApiVersion("1")]
    public class Users : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<Users> logger;
        private readonly IJwtInteface jwtService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public Users(IUserRepository userRepository,
            ILogger<Users> logger, 
            IJwtInteface jwtService,
            UserManager<User> userManager,
            RoleManager<Role> roleManager, 
            SignInManager<User> signInManager)
        {
            this.userRepository = userRepository;
            this.logger = logger;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public virtual async Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken)
        {
            //var userName = HttpContext.User.Identity.GetUserName();
            //userName = HttpContext.User.Identity.Name;
            //var userId = HttpContext.User.Identity.GetUserId();
            //var userIdInt = HttpContext.User.Identity.GetUserId<int>();
            //var phone = HttpContext.User.Identity.FindFirstValue(ClaimTypes.MobilePhone);
            //var role = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Role);

            var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public virtual async Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            var user2 = await userManager.FindByIdAsync(id.ToString());
            var role = await roleManager.FindByNameAsync("Admin");

            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
                return NotFound();

            await userManager.UpdateSecurityStampAsync(user);
            //await userRepository.UpdateSecurityStampAsync(user, cancellationToken);

            return user;
        }

        /// <summary>
        /// This method generate JWT Token
        /// </summary>
        /// <param name="tokenRequest">The information of token request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public virtual async Task<ActionResult> Token([FromForm] TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            if (!tokenRequest.Grant_type.Equals("password", StringComparison.OrdinalIgnoreCase))
                throw new Exception("OAuth flow is not password.");

            //var user = await userRepository.GetByUserAndPass(username, password, cancellationToken);
            var user = await userManager.FindByNameAsync(tokenRequest.Username);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var isPasswordValid = await userManager.CheckPasswordAsync(user, tokenRequest.Password);
            if (!isPasswordValid)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");


            //if (user == null)
            //    throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var jwt = jwtService.Generate(user , new Role());
            return new JsonResult(jwt);
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            logger.LogError("متد Create فراخوانی شد");
            HttpContext.RiseError(new Exception("متد Create فراخوانی شد"));

            //var exists = await userRepository.TableNoTracking.AnyAsync(p => p.UserName == userDto.UserName);
            //if (exists)
            //    return BadRequest("نام کاربری تکراری است");


            var user = new User
            {
                Age = userDto.Age,
                FullName = userDto.FullName,
                Gender = userDto.Gender,
                UserName = userDto.UserName,
                Email = userDto.Email
            };
            var result = await userManager.CreateAsync(user, userDto.Password);

            var result2 = await roleManager.CreateAsync(new Role
            {
                Name = "Admin",
                Description = "admin role"
            });

            var result3 = await userManager.AddToRoleAsync(user, "Admin");

            //await userRepository.AddAsync(user, userDto.Password, cancellationToken);
            return user;
        }

        [HttpPut]
        public virtual async Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            var updateUser = await userRepository.GetByIdAsync(cancellationToken, id);

            updateUser.UserName = user.UserName;
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.FullName = user.FullName;
            updateUser.Age = user.Age;
            updateUser.Gender = user.Gender;
            updateUser.IsActive = user.IsActive;
            updateUser.LastLoginDate = user.LastLoginDate;

            await userRepository.UpdateAsync(updateUser, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public virtual async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            await userRepository.DeleteAsync(user, cancellationToken);

            return Ok();
        }

    }
}
