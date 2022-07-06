using System;
using System.Threading.Tasks;
using client.application.Execptions;
using client.application.Features.Account;
using client.core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace client.api.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }
        
        //crud account
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                var account = await _accountService.GetAccountAsync(id);
                return Ok(account);
            }
            catch (NotFoundExepction ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            try
            {
                var newAccount = await _accountService.CreateAccountAsync(account);
                return Ok(newAccount);
            }
            catch (BadRequestExeption ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] Account account)
        {
            try
            {
                account.Id = id;
                var updatedAccount = await _accountService.UpdateAccountAsync( account);
                return Ok(updatedAccount);
            }
            catch (NotFoundExepction ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (BadRequestExeption ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                await _accountService.DeleteAccountAsync(id);
                return Ok();
            }
            catch (NotFoundExepction ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}