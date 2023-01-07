using BankAccount.Data.Entity;
using BankAccount.Data.Model;
using BankAccount.Services.Interfaces;
using BankAccount.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BankAccount.Api.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class UserController : HttpControllerBase
    {
        private IUserServices _userServices;
        private IAccountServices _accountServices;
        private ILogger<UserController> _logger;

        public UserController(IUserServices userServices, IAccountServices accountServices, ILogger<UserController> logger)
        {
            _userServices = userServices;
            _accountServices = accountServices;
            _logger = logger;
        }

        private void LogException(Exception ex, [CallerMemberName] string methodName = "")
        {
            _logger.LogError("An error happened at " + methodName + " due to " + ex.Message + " exception details: " +
            ex.InnerException + " Stack trace " + ex.StackTrace);
        }

        [HttpGet("Email/{email}")]
        public IActionResult Get([FromRoute] string email)
        {
            try
            {
                if(!_userServices.Exist(x=>x.email.ToUpper() == email.ToUpper()))
                {
                    return NotFoundResponse();
                }

                return OkResponse(_userServices.GetUser(x=>x.email == email.ToLower()));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [HttpGet("Exist/{id}")]
        public IActionResult Exist([FromRoute] Guid id)
        {
            try
            {
                return OkResponse(_userServices.Exist(id));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequestResponse(ModelState);
            }

            try
            {
                var  login =_userServices.GetLogin(model.Email, model.Password);
                LoginResponseModel response = new();
                response.Result = login;
                if (login == LoginResponseEnum.OK)
                {
                    response.Id = _userServices.GetUserByEmail(model.Email).UserId;
                }
                return OkResponse(response);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]Guid id) 
        {
            try
            {
                if (!_userServices.Exist(id))
                     return NotFoundResponse();

                return OkResponse(_userServices.GetUser(id));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
               if(_userServices.Exist(x=>x.email.ToLower() == user.email.ToLower()))
                    return OkResponse(_userServices.GetUser(x=>x.email.ToLower() == user.email.ToLower()));

                return OkResponse(_userServices.Add(user));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] User user, [FromRoute]Guid id)
        {
            try
            {
                user =_userServices.Update(user,id);

                return OkResponse(user);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }


        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                return OkResponse(_userServices.GetUsers());
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }


        [HttpDelete("id")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                if (!_userServices.Exist(id))
                    return NotFoundResponse();

                _userServices.Delete(id);
                foreach (var account in _accountServices.GetAccounts(x=>x.UserId==id))
                {
                    try
                    {
                        _accountServices.Delete(account.guid);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return OkResponse(_userServices.GetUser(id));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

    }
}
