using EmployeeInsight.Crawler.Configurations;
using EmployeeInsight.Crawler.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EmployeeInsight.Crawler.Repository
{
    public class EmployeeCatalogProvider : IEmployeeCatalogProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly EmployeeCatalogProviderConfiguration _employeesCatalogProviderConfig;
        private readonly ILogger<EmployeeCatalogProvider> _logger;
        private HttpClient _httpClient;

        public EmployeeCatalogProvider(IHttpClientFactory httpClientFactory, IOptions<EmployeeCatalogProviderConfiguration> employeesCatalogProviderConfig,
            ILogger<EmployeeCatalogProvider> logger)
        {
            _httpClientFactory = httpClientFactory;
            _employeesCatalogProviderConfig = employeesCatalogProviderConfig.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>?> GetEmployeesData(CancellationToken cancellationToken)
        {
            IEnumerable<Employee> employees = null;
            try
            {
                using (_httpClient = _httpClientFactory.CreateClient("HttpClient"))
                {
                    var response = await _httpClient.GetAsync($"{_employeesCatalogProviderConfig.BaseAddress}?code={_employeesCatalogProviderConfig.Code}");
                    response.EnsureSuccessStatusCode();

                    var employeesData = JsonSerializer.Deserialize<IEnumerable<EmployeeJsonResponse>?>(await response.Content.ReadAsStringAsync());

                    if (employeesData is not null && employeesData.Any())
                    {
                        employees = employeesData
                            .GroupBy(e => e.EmployeeName)
                            .Select(group => new Employee
                            {
                                Name = group.Key,
                                Duration = Math.Round(group.Sum(x => (x.EndTimeUtc - x.StarTimeUtc).TotalHours), 2)
                            });
                        return employees.OrderByDescending(x => x.Duration);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return employees;
        }
    }
}
