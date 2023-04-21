using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerApp.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using DinnerApp.Application.Common.Errors;
using FluentResults;
using ErrorOr;
using DinnerApp.Domain.Common.Errors;
using MediatR;
using DinnerApp.Application.Authentication.Commands.Register;
using DinnerApp.Application.Authentication.Queries.Login;
using DinnerApp.Application.Authentication.Common;

namespace DinnerApp.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController // This class inherits from the "ApiController" class not ControllerBase
    {
        
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request){

            var command = new RegisterCommand(request.FirstName,request.LastName,request.Email,request.Password);
            ErrorOr<AuthenticationResult> authResult = await  _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors)
                );    
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request){

            var query = new LoginQuery(request.Email, request.Password);
            var loginResult = await _mediator.Send(query);

            if (loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidCredentials) {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: loginResult.FirstError.Description
                    );
            }
            
            return loginResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors)
                );
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
            );
        }

    }
}