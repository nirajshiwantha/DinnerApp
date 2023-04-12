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

namespace DinnerApp.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request){
            
            Result<AuthenticationResult> registerResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            if (registerResult.IsSuccess)
            {
                return Ok(MapAuthResult(registerResult.Value));
            }
            var firstError = registerResult.Errors[0];

            if (firstError is DuplicateEmailError) 
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Email Already Exisist!");
            }

            return Problem();      
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

        [HttpPost("login")]
        public IActionResult Register(LoginRequest request){


            Result<AuthenticationResult > loginResult = _authenticationService.Login(
                request.Email,
                request.Password);

            if (loginResult.IsSuccess)
            {
                return Ok(MapAuthResult(loginResult.Value));
            }

            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Internal Server Error!");
        }
    
    }
}