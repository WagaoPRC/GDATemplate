using System;
using Microsoft.Extensions.DependencyInjection;

namespace GDATemplate.Api.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection Services, IConfiguration configuration)
        {
            if (Services == null)
                throw new ArgumentNullException(nameof(Services));

            Services.AddHealthChecks();

            return Services;
        }
    }
}
