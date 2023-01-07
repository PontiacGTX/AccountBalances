using BankAccount.Data;
using BankAccount.Presentation.Common.Helpers;
using BankAccount.Presentation.Models;
using BankAccount.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankAccount.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IUserServices _userServices;

        public HomeController(IUserServices userServices, SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger)
        {
            _userServices = userServices;
            _logger = logger;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Index()
        {

            BankAccount.Data.Entity.User user = null;
            if (!string.IsNullOrEmpty(User?.Identity?.Name))
            {
                var res =(await _userServices.GetUser(User?.Identity?.Name));
                if(res.StatusCode==200)
                {
                    user = res.Data.CastJsonAs<BankAccount.Data.Entity.User>();
                }
            }
            return View(user);
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}