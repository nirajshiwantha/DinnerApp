using DinnerApp.Application.Common.Interfaces.Authentication;
using DinnerApp.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinnerApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration) 
        {
            services.Configure<JWTSettings>(configuration.GetSection(JWTSettings.SectionName));
            services.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();
            return services;
        }
    }
}