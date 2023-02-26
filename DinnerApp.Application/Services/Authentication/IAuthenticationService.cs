using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinnerApp.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string  firstName,string lastName, string email, string password);
        AuthenticationResult Login(string email, string password);

    }
}