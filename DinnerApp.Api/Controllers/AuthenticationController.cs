using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerApp.Application.Services.Authentication;
using DinnerApp.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using DinnerApp.Application.Common.Errors;


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
            
            OneOf<AuthenticationResult, DuplicateEmailError> registerResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            return registerResult.Match(
                    authResult => Ok(MapAuthResult(authResult)),
                    _ => Problem(statusCode: StatusCodes.Status409Conflict, title: "Email Already Exisist!"));      
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


            OneOf<AuthenticationResult, DuplicateEmailError> loginResult = _authenticationService.Login(
                request.Email,
                request.Password);

            if (loginResult.IsT0)
            {
                var authResult = loginResult.AsT0;
                var response = new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);
                return Ok(response);
            }

            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Internal Server Error!");
        }
    
    }
}