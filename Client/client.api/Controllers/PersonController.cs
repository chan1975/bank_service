using System;
using System.Threading.Tasks;
using client.application.Execptions;
using client.application.Features.Person;
using client.core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace client.api.Controllers
{
    [ApiController]
    [Route("api/personas")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
            
        }
        //crud
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _personService.GetPeople());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var person = await _personService.GetPerson(id);
                return Ok(person);
            }
            catch (NotFoundExepction ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            return Ok(await _personService.CreatePerson(person));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person person)
        {
            person.Id = id;
            return Ok(await _personService.UpdatePerson(person));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _personService.DeletePerson(id));
            }
            catch (NotFoundExepction ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            
        }
    }
}