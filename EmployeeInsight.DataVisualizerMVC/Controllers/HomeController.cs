using EmployeeInsight.Crawler.Repository;
using EmployeeInsight.DataVisualizerMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeInsight.DataVisualizerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeCatalogProvider _employeeCatalogProvider;

        public HomeController(ILogger<HomeController> logger, IEmployeeCatalogProvider employeeCatalogProvider)
        {
            _logger = logger;
            _employeeCatalogProvider = employeeCatalogProvider;
        }

        public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            var employeeCatalog = new EmployeeCatalogViewModel()
            {
                Employees = await _employeeCatalogProvider.GetEmployeesData(cancellationToken)
            };

            return View(employeeCatalog);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
