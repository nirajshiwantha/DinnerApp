using DinnerApp.Api.Common.Errors;
using DinnerApp.Api.Common.Mappling;
using DinnerApp.Application.Authentication.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DinnerApp.Pre
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, DinnerAppProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}
