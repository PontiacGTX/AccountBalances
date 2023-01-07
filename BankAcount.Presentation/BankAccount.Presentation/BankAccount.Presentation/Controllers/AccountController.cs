using BankAccount.Common.Helpers;
using BankAccount.Common.Responses;
using BankAccount.Data;
using BankAccount.Data.Entity;
using BankAccount.Data.Model;
using BankAccount.Presentation.Common.Helpers;
using BankAccount.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace BankAccount.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IUserServices _userServices;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IUserServices userServices)
        {
            _userManager = userManager;
            _signInManager=signInManager;
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detalles()
        {
            try
            {
                if (TempData["ValErrors"] == null)
                {
                    TempData["ValErrors"] = System.Text.Json.JsonSerializer.Serialize(new List<string>());
                }
                var email = User.Identity.Name;
                var response = (await _userServices.GetUser(email));
                BankAccount.Data.Entity.User user = null;
                if(response.StatusCode!=200)
                {
                   return RedirectToAction("Index", "Home");
                }

                user=  response.Data.CastJsonAs<BankAccount.Data.Entity.User>();

                return View(user);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detalles([FromBody] BankAccount.Data.Entity.User u)
        {
            try
            {
                
                ModelState.Remove<BankAccount.Data.Entity.User>(x => x.Accounts);
                if (!ModelState.IsValid)
                { 
                    return View(u);
                }                
                

                var email = User.Identity.Name;
                var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.NormalizedEmail.ToUpper() ==  u.email.ToUpper());
                IdentityResult? result=null;
                if (user! != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    result = await _userManager.ResetPasswordAsync(user, token, u.password);
                }
                if (result ==null || !result.Succeeded)
                {
                    IEnumerable<string> errors = null;
                    if (TempData["ValErrors"] == null)
                        TempData["ValErrors"] = System.Text.Json.JsonSerializer.Serialize(new List<string>().Select(x=>x).ToList());

                   
                    errors = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<string>>(TempData["ValErrors"].ToString()) as IEnumerable<string>;

                    var lError = (new List<string>());

                    lError.Add("Contraseña con formato invalid");
                    if(result!=null)
                    {
                        lError.AddRange(result.Errors.Select(x => x.Description));
                    }
                    TempData["ValErrors"] = System.Text.Json.JsonSerializer.Serialize(lError.Concat(errors).Select(x => x).ToList());

                    var dat = (await _userServices.GetUser(email));
                    var usr = dat.Data.CastJsonAs<BankAccount.Data.Entity.User>();
                    return  Ok(Factory.GetResponse<ServerErrorResponse>(usr, 400,"Validation Errors",false, lError));
                
                }

                var response = (await _userServices.Update(u, u.UserId));
                if (response.StatusCode!=200)
                {
                    if(response.StatusCode==400 || response.StatusCode==500)
                    {
                        if(response.StatusCode == 500)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        IEnumerable<string> errors = null;
                        if (TempData["ValErrors"] == null)
                            TempData["ValErrors"] = System.Text.Json.JsonSerializer.Serialize(new List<string>().Select(x => x).ToList());


                        errors = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<string>>(TempData["ValErrors"].ToString()) as IEnumerable<string>;

                        var lError = (new List<string>());

                        lError.Add("Contraseña con formato invalid");
                        if (result != null)
                        {
                            lError.AddRange(result.Errors.Select(x => x.Description));
                        }
                        TempData["ValErrors"] = System.Text.Json.JsonSerializer.Serialize(lError.Concat(errors).Concat(response.Validation).Select(x => x).ToList());

                        var dat = (await _userServices.GetUser(email));
                        var usr = dat.Data.CastJsonAs<BankAccount.Data.Entity.User>();
                        return Ok(Factory.GetResponse<ServerErrorResponse>(usr, 400, "Validation Errors", false, lError));
                    }
                }

                TempData["ValErrors"] = null;
                return Ok(response);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            ViewBag.Users = (await _userServices.GetAll()).Data.CastJsonAs<List<BankAccount.Data.Entity.User>>();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginModel model)
        {
            if(!ModelState.IsValid) 
            {
                return View(model);
            }

            try
            {
                
                var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.NormalizedEmail== model.Email.ToUpper());
                if(user == null)
                {
                    ModelState.AddModelError("email", "email not found");
                    return View(model);
                }
                var result =await _signInManager.PasswordSignInAsync(user,model.Password, true,false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("email","Invalid email");
                    ModelState.AddModelError("password","Invalid password");
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
