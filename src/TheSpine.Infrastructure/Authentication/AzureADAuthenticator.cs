using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using System.Security.Claims;
using TheSpine.Application.Constants;
using TheSpine.Infrastructure.Authentication.Configuration;
using TheSpine.Infrastructure.DataAccess.Graph;

namespace TheSpine.Infrastructure.Authentication
{
    public static class AzureADAuthenticator
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApp(options =>
                    {
                        var adConfiguration = configuration.GetSection("AzureAd").Get<AuthenticationConfiguration>();

                        options.Domain = adConfiguration.Domain;
                        options.TenantId = adConfiguration.TenantId;
                        options.ClientId = adConfiguration.ClientId;
                        options.Instance = adConfiguration.Instance;
                        options.ClientSecret = adConfiguration.ClientSecret;
                        options.CallbackPath = adConfiguration.CallbackPath;
                        options.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                        options.Events = new OpenIdConnectEvents
                        {
                            OnTokenValidated = async context => 
                            {
                                var ctx = context;
                            },
                            OnTicketReceived = ConfigureUserRole
                        };
                    });
        }

        private static async Task ConfigureUserRole(TicketReceivedContext context)
        {
            string email = context.Principal.FindFirstValue("preferred_username");

            if (string.IsNullOrEmpty(email)) return;

            var graphService = context.HttpContext.RequestServices.GetRequiredService<IGraphService>();
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, Roles.Moderator)
                };
            if (await graphService.IsModerator(email))
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, Roles.Moderator)
                };

                var appIdentity = new ClaimsIdentity(claims);
                context.Principal.AddIdentity(appIdentity);
            }
        }

    }
}
