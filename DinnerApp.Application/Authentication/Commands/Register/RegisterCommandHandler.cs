using DinnerApp.Application.Common.Interfaces.Authentication;
using DinnerApp.Application.Common.Interfaces.Persistance;
using DinnerApp.Domain.Entities;
using DinnerApp.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinnerApp.Application.Authentication.Common;

namespace DinnerApp.Application.Authentication.Commands.Register
{
    internal class RegisterCommandHandler : 
        IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(
            IJWTTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            //validate the user does not exists
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }


            //create user(geberate unique ID) and persist to database
            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };

            _userRepository.Add(user);

            //create jwt Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
