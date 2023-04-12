using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerApp.Application.Services.Authentication;
using DinnerApp.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using DinnerApp.Application.Common.Errors;
using FluentResults;
using ErrorOr;
using DinnerApp.Domain.Common.Errors;

namespace DinnerApp.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController // This class inherits from the "ApiController" class not ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request){

            var authResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors)
                );    
        }


        [HttpPost("login")]
        public IActionResult Login(LoginRequest request){


            var loginResult = _authenticationService.Login(
                request.Email,
                request.Password);

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