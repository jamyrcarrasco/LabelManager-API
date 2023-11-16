using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace LABEL_MANAGER.Middlewares
{
    public class JWTREsolver : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IConfiguration _configuration;
        public JWTREsolver(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail("Favor incluir header en la autenticacion"));
            }

            var keyArray = authorization.FirstOrDefault().Split(" ");
            if (keyArray.Count() != 2)
            {
                return Task.FromResult(AuthenticateResult.Fail("Header Invalido"));
            }

            if (!keyArray[0].Equals(JwtBearerDefaults.AuthenticationScheme, StringComparison.InvariantCultureIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Esquema Invalido"));

            }

            if (!TokenValid(keyArray[1]))
            {
                return Task.FromResult(AuthenticateResult.Fail("Esquema Invalido"));
            }

            var indentities = new List<ClaimsIdentity> { new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme) };
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(indentities), JwtBearerDefaults.AuthenticationScheme);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private bool TokenValid(string token)
        {
            bool isAuthentication, timeExpired = false;
            try
            {
                var timeExpire = new JwtSecurityTokenHandler().ReadToken(token).ValidTo;
                if (timeExpire > DateTime.Now)
                    timeExpired = true;

                var param = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateTokenReplay = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:key").Value)),
                    ValidIssuer = _configuration.GetSection("Jwt:Issuer").Value,
                    ValidAudience = _configuration.GetSection("Jwt:Audience").Value,
                };

                var claim = new JwtSecurityTokenHandler().ValidateToken(token, param, out _);
                isAuthentication = claim is not null ? claim.Identity.IsAuthenticated : false;
                return timeExpired && isAuthentication;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
