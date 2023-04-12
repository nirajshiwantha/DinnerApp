using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerApp.Application.Common.Errors;
using DinnerApp.Application.Common.Interfaces.Authentication;
using DinnerApp.Application.Common.Interfaces.Persistance;
using DinnerApp.Domain.Common.Errors;
using DinnerApp.Domain.Entities;
using ErrorOr;

namespace DinnerApp.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJWTTokenGenerator jwtTokenGenerator, IUserRepository userRepository){
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }



        public ErrorOr<AuthenticationResult > Register(string firstName, string lastName, string email, string password)
        {

            //validate the user does not exists
            if (_userRepository.GetUserByEmail(email) is not null) 
            {
                return Errors.User.DuplicateEmail;
            }


            //create user(geberate unique ID) and persist to database
            var user = new User 
            { 
                FirstName = firstName,
                LastName = lastName,
                Email = email, 
                Password = password 
            };

            _userRepository.Add(user);

            //create jwt Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public ErrorOr<AuthenticationResult > Login(string email, string password)
        {
            // Validate the user exists
            if(_userRepository.GetUserByEmail(email) is not User user) 
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // validate the password
            if (user.Password != password) 
            {
                return new[] { Errors.Authentication.InvalidCredentials };
            }

            //create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}