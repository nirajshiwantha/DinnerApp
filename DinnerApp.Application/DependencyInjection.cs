using DinnerApp.Application.Authentication.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DinnerApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR with the service collection and configure it to register all the necessary
            // services in the same assembly as this class (i.e., the Application layer)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // Return the service collection after the necessary dependencies for the Application layer have been registered
            return services;
        }
    }
}
