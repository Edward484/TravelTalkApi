using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TravelTalkApi.Constants
{
    public class AuthConfig
    {
        public static TokenValidationParameters JWTValidationConfig = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secrets.AuthSignature)),
            ValidateIssuerSigningKey = true
        };
    }
}