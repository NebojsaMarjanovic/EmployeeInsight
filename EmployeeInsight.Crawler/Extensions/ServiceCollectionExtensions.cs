using EmployeeInsight.Crawler.Configurations;
using EmployeeInsight.Crawler.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EmployeeInsight.Crawler.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCrawler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("HttpClient")
                .AddPolicyHandler(PolicyExtensions.GetHttpRetryPolicy());
            services.AddOptions<EmployeeCatalogProviderConfiguration>().Bind(configuration.GetSection("EmployeeCatalogProvider"));
            services.AddMemoryCache();
            services.TryAddScoped<IEmployeeCatalogProvider, Repository.EmployeeCatalogProvider>();
            services.TryDecorate<IEmployeeCatalogProvider, Crawler.Repository.Cache.EmployeeCatalogProvider>();

            return services;
        }
    }
}
