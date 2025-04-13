using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.MVC.Models;
using System.Security.Claims;

namespace ECommerce.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICookieAuthService _authService;

        public AccountController(ICookieAuthService authService)
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
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
                return View(model);
            }

            // CookieAuthService created cookie in LoginAsync
            // Can check with IsSignedInAsync or redirect directly
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
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
                UserId    = Guid.Parse(userId!),
                Email     = email!,
                FirstName = first!,
                LastName  = last!
            };
            return View(model);
        }
    }
}
