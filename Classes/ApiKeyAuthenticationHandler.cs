
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MIFin.Api.Data;

// https://www.endpointdev.com/blog/2022/06/implementing-authentication-in-asp.net-core-web-apis/#table-of-contents
namespace MIFinApi.Authentication {
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
        private const string API_KEY_HEADER = "ApiKey";

        private readonly DataRepository _dataRepository;
        public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
                        UrlEncoder encoder, ISystemClock clock, DataRepository dataRepository
        ) : base(options, logger, encoder, clock) {
            _dataRepository = dataRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
            if (!Request.Headers.ContainsKey(API_KEY_HEADER)) {
                return AuthenticateResult.Fail("Header Not Found.");
            }

            string apiKeyToValidate = Request.Headers[API_KEY_HEADER]!;

            string userName = _dataRepository.GetUserNameByToken(apiKeyToValidate);
            if (string.IsNullOrEmpty(userName)) {
                return AuthenticateResult.Fail("Invalid key.");
            }

            IdentityUser tmpUser = new IdentityUser() { UserName = userName };
            return AuthenticateResult.Success(CreateTicket(tmpUser));
        }

        private AuthenticationTicket CreateTicket(IdentityUser user) {
            var claims = new[] {
              //  new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!),
               // new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return ticket;
        }
    }
}


/*
 
  //var apiKey = await _context.UserApiKeys
            //    .Include(uak => uak.User)
            //    .SingleOrDefaultAsync(uak => uak.Value == apiKeyToValidate);

            //if (apiKey == null) {
            //    return AuthenticateResult.Fail("Invalid key.");
            //}
            // IdentityUser tmpUser = new IdentityUser() { Id="1", UserName="system", Email="ddd@eee.com"};
 */