using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerApp.Application.Common.Errors;
using OneOf;

namespace DinnerApp.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        OneOf<AuthenticationResult, DuplicateEmailError> Register(string  firstName,string lastName, string email, string password);
        OneOf<AuthenticationResult, DuplicateEmailError > Login(string email, string password);

    }
}