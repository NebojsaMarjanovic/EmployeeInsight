using Microsoft.AspNetCore.Mvc;

namespace EmployeeInsight.ChartGeneratorAPI.Services
{
    public interface IChartGenerator
    {
        Task<FileStreamResult> GeneratePieChart(CancellationToken cancellationToken);
    }
}
