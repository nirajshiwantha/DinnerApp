using DinnerApp.Application.Authentication.Commands.Register;
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

namespace DinnerApp.Application.Authentication.Queries.Login
{
    internal class LoginCommandHandler :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(
            IJWTTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Validate the user exists
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // validate the password
            if (user.Password != query.Password)
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
