using System;
using GDATemplate.Api.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GDATemplate.Api.Extensions
{
    public static class PolicyExtensions
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //Cria as policies com o mesmo nome dos escopos
            services.AddAuthorization(options =>
            {
                options.AddPolicy("example.search", policy => policy.Requirements.Add(new HasScopeRequirement("example.search", configuration["LoginSP:Authority"])));
                options.AddPolicy("example.upsert", policy => policy.Requirements.Add(new HasScopeRequirement("example.upsert", configuration["LoginSP:Authority"])));
            });

            // Registra o handler que valida o escopo
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            return services;
        }
    }
}
