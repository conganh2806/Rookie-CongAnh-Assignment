using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.MVC.Models;
using System.Security.Claims;
using YourProjectNamespace.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace ECommerce.MVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ICookieAuthService _authService;

        public AccountController(ICookieAuthService authService, ILogger<AccountController> logger) 
            : base(logger)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
                
            var request = new LoginRequest
            {
                Email = model.Email,
                Password = model.Password
            };

            var result = await _authService.LoginAsync(request);

            if (result == null)
            {
                ModelState.AddModelError("", "Email or password is incorrect.");
                return View(model);
            }
            System.Console.WriteLine("Login successful, redirecting to Profile");
            return RedirectToAction("Profile", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> Profile()
        {
            if (!await _authService.IsSignedInAsync())
                return RedirectToAction("Login");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email  = User.FindFirst(ClaimTypes.Email)?.Value;
            var first  = User.FindFirst(ClaimTypes.GivenName)?.Value;
            var last   = User.FindFirst(ClaimTypes.Surname)?.Value;

            var model = new ProfileViewModel
            {
                UserId    = userId!,
                Email     = email!,
                FirstName = first!,
                LastName  = last!
            };
            return View(model);
        }
    }
}
