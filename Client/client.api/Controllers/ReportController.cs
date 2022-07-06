using System;
using System.Threading.Tasks;
using client.application.Execptions;
using client.application.Features.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace client.api.Controllers
{
    [ApiController]
    [Route("api/reportes")]
    public class ReportController: ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(int id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var report = await _reportService.GetReportByDates(id, startDate, endDate);
                return Ok(report);
            }
            catch (BadRequestExeption ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}