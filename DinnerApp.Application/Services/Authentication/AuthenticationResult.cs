using DinnerApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinnerApp.Application.Services.Authentication
{
    public record AuthenticationResult(
        User User, 
        string Token
    );
}