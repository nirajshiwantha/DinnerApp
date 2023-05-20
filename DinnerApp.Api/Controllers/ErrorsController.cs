using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DinnerApp.Application.Common.Errors;


namespace DinnerApp.Api.Controllers
{
    // Define a new controller class "ErrorsController"
    public class ErrorsController : ControllerBase
    {
        // Map the "/error" URL path to the "Error" action method
        [Route("/error")]
        public IActionResult Error()
        {
            // Get the exception object from the current request context
            //Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            // Create a new ProblemDetails object
            return Problem();
        }
    }
}
