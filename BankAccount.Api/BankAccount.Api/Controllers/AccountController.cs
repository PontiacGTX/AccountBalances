using BankAccount.Data.Entity;
using BankAccount.Data.Model;
using BankAccount.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BankAccount.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : HttpControllerBase
    {
        private IAccountServices _accountServices;
        private ILogger<AccountController> _logger;

        public AccountController(IAccountServices accountServices, ILogger<AccountController> logger)
        {
            _accountServices = accountServices;
            _logger = logger;
        }

        private void LogException(Exception ex, [CallerMemberName] string methodName = "")
        {
            _logger.LogError("An error happened at " + methodName + " due to " + ex.Message + " exception details: " +
            ex.InnerException + " Stack trace " + ex.StackTrace);
        }
        [HttpGet("Exist/{id}")]
        public IActionResult Exist([FromRoute] Guid id)
        {
            try
            {
                return OkResponse(_accountServices.Exist(id));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                if (!_accountServices.Exist(id))
                    return NotFoundResponse();

                return OkResponse(_accountServices.GetAccount(id));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Account account)
        {
            try
            {
                if (_accountServices.Exist(x => x.guid == account.guid))
                {
                    return BadRequestResponse(new List<string> { "Already exist account with guid"},409);
                }
                   

                return OkResponse(_accountServices.Add(account));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Account account, [FromRoute] Guid id)
        {
            try
            {
                account = _accountServices.Update(account, id);

                return OkResponse(account);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                
                return OkResponse(_accountServices.Delete(id));
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
                return OkResponse(_accountServices.GetAccounts());
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }
    }
}
