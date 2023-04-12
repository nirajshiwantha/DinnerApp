using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerApp.Application.Common.Errors;
using FluentResults;

namespace DinnerApp.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        Result<AuthenticationResult> Register(string  firstName,string lastName, string email, string password);
        Result<AuthenticationResult> Login(string email, string password);

    }
}