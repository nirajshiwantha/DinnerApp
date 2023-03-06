using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinnerApp.Infrastructure.Authentication
{
    public class JWTSettings
    {
        public const string SectionName = "JwtSettings";
        public string Secret { get; init; } = string.Empty;
        public int ExpiryMinutes { get; init; }
        public string Issuer { get; init; } = string.Empty;
        public string Audiance { get; init; } = string.Empty;
        
    }
}