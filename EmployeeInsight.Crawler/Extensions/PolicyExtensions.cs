using Polly;
using Polly.Extensions.Http;

namespace EmployeeInsight.Crawler.Extensions
{
    //extension for registering polly policy 
    public static class PolicyExtensions
    {
        public static IAsyncPolicy<HttpResponseMessage> GetHttpRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<HttpRequestException>()
                .OrResult(response => response.StatusCode != System.Net.HttpStatusCode.OK)
                .WaitAndRetryAsync(2, retryCount => TimeSpan.FromSeconds(5));
        }
    }
}
