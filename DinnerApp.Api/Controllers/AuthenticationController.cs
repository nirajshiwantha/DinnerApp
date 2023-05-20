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
using MapsterMapper;

namespace DinnerApp.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController // This class inherits from the "ApiController" class not ControllerBase
    {
        
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request){

            var command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> authResult = await  _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors)
                );    
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request){

            var query = _mapper.Map<LoginQuery>(request);
            var loginResult = await _mediator.Send(query);

            if (loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidCredentials) {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: loginResult.FirstError.Description
                    );
            }
            
            return loginResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors)
                );
        }
    }
}