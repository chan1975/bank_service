using System;
using System.Threading.Tasks;
using client.application.Execptions;
using client.application.Features.Transaction;
using client.core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace client.api.Controllers
{
    [ApiController]
    [Route("api/movimientos")]
    public class TransactionController:ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;


        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        //crud movimientos
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transactionDto)
        {
            try
            {
                var transaction = await _transactionService.CreateTransaction(transactionDto);
                return Ok(transaction);
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
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                await _transactionService.DeleteTransaction(id);
                return Ok();
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
    }
}