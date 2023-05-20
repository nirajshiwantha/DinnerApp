using DinnerApp.Application.Authentication.Common;
using DinnerApp.Contracts.Authentication;
using Mapster;

namespace DinnerApp.Api.Common.Mappling
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.User);
        }
    }
}
