using EmployeeInsight.ChartGeneratorAPI.Services;
using EmployeeInsight.Crawler.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInsight.ChartGeneratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IChartGenerator _chartGenerator;

        public ChartController(IChartGenerator chartGenerator)
        {
            _chartGenerator = chartGenerator;
        }

        [HttpGet(Name = "GetChart")]
        public async Task<FileStreamResult> GetAsync(CancellationToken cancellationToken)
        {
            return await _chartGenerator.GeneratePieChart(cancellationToken);
        }
    }
}
