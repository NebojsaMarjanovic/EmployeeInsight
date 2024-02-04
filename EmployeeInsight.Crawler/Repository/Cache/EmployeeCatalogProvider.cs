using EmployeeInsight.Crawler.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EmployeeInsight.Crawler.Repository.Cache
{
    //used Scrutor nuget package for implementing decorater pattern:
    //  - expanding main functionality (data fetching from rest api) with fetching from cache if the data is cached
    public class EmployeeCatalogProvider : IEmployeeCatalogProvider
    {
        private readonly IMemoryCache _memoryCache;
        private readonly Repository.IEmployeeCatalogProvider _employeesDataProvider;

        public EmployeeCatalogProvider(IMemoryCache memoryCache, IEmployeeCatalogProvider employeesDataProvider)
        {
            _memoryCache = memoryCache;
            _employeesDataProvider = employeesDataProvider;
        }

        public async Task<IEnumerable<Employee>?> GetEmployeesData(CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue("ApiData", out IEnumerable<Employee>? employees))
            {
                employees = await _employeesDataProvider.GetEmployeesData(cancellationToken);

                if (employees is not null)
                {
                    _memoryCache.Set("ApiData", employees, TimeSpan.FromHours(1));
                }
            }
            return employees;
        }
    }
}
