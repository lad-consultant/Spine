using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System.Reflection;
using TheSpine.Application.CustomAttributes;

namespace TheSpine.Application.Behaviors
{
    public class AuthorizationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly ILogger<AuthorizationPipelineBehavior<TRequest, TResponse>> logger;

        public AuthorizationPipelineBehavior(
            AuthenticationStateProvider authenticationStateProvider,
            ILogger<AuthorizationPipelineBehavior<TRequest, TResponse>> logger)
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle the authorization request.");

            var authorizeAttribute = typeof(TRequest).GetCustomAttribute<AuthorizeAttribute>();

            if (authorizeAttribute != null)
            {
                var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();

                var user = authenticationState.User;

                if (!user.Identity.IsAuthenticated || !user.IsInRole(authorizeAttribute.Role))
                {
                    logger.LogError("Unauthorized access");

                    throw new UnauthorizedAccessException("Unauthorized access");
                }
            }

            logger.LogInformation("Authorization succeded.");

            return await next();
        }
    }
}
