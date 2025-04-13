using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce.MVC.Models;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ECommerce.Domain.Entities.ApplicationUser;
using System.Threading.Tasks;

namespace ECommerce.MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<User> _userManager;

        public HomeController(ICategoryService categoryService,
                             IProductService productService,
                            ILogger<HomeController> logger,
                            UserManager<User> userManager)
            : base(logger)
        {
            _categoryService = categoryService;
            _productService = productService;
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {   
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? 
                HttpContext.TraceIdentifier 
            });
        }
    }
}