using GDATemplate.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GDATemplate.Api.Extensions
{
    public static class ContextExtensions
    {
        public static void AddContexts(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SqlContext>(options =>
                options.UseSqlServer(configuration["BancoTemplate"]));

        }
    }
}
