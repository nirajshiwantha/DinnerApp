using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerApp.Application.Common.Interfaces.Authentication;

namespace DinnerApp.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJWTTokenGenerator jwtTokenGenerator){
            _jwtTokenGenerator = jwtTokenGenerator;
        }



        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {

            //check if user exists

            //create user(geberate unique ID)

            //create jwt Token
            var userId = Guid.NewGuid();

            var token = _jwtTokenGenerator.GenerateToken(userId,firstName,lastName);

            return new AuthenticationResult(
                userId, 
                firstName,
                lastName,
                email,
                token);
        }

        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(
                Guid.NewGuid(), 
                "firstName",
                "lastName",
                email,
                "Token");
        }
    }
}