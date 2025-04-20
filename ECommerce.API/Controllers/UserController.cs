using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;

        public UserController(ILogger<User> logger, IUserService userService) 
        : base(logger)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Success(users, "Get all users successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) 
            {
                return Error("User not found", HttpStatusCode.NotFound);
            }

            return Success(user, "Get user successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid) return Error("Error validate", HttpStatusCode.BadRequest);

            var user = await _userService.UpdateAsync(id, request);

            if (user == null) 
            {
                return Error("User not found", HttpStatusCode.NotFound);
            }

            return Success(user, "Update user successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);

            return Success<User?>(null, "Delete user successfully.");  
        }

    }
}