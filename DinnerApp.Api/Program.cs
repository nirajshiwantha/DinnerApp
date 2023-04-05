using DinnerApp.Api.Errors;
using DinnerApp.Api.Filters;
using DinnerApp.Api.Middleware;
using DinnerApp.Application;
using DinnerApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, DinnerAppProblemDetailsFactory>();
}

var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();  
    app.Run();
}
