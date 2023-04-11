using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerApp.Application.Common.Errors;
using DinnerApp.Application.Common.Interfaces.Authentication;
using DinnerApp.Application.Common.Interfaces.Persistance;
using DinnerApp.Domain.Entities;

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



        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {

            //validate the user does not exists
            if (_userRepository.GetUserByEmail(email) is not null) 
            {
                throw new DuplicateEmailException();
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

        public AuthenticationResult Login(string email, string password)
        {
            // Validate the user exists
            if(_userRepository.GetUserByEmail(email) is not User user) 
            {
                throw new Exception("User with given email does not exist!");
            }

            // validate the password
            if (user.Password != password) 
            {
                throw new Exception("Incorrect Passowrd!");
            }

            //create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}