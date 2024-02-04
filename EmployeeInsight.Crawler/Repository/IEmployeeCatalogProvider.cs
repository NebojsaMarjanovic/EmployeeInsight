using EmployeeInsight.Crawler.Models;

namespace EmployeeInsight.Crawler.Repository
{
    public interface IEmployeeCatalogProvider
    {
        Task<IEnumerable<Employee>?> GetEmployeesData(CancellationToken cancellationToken);
    }
}
