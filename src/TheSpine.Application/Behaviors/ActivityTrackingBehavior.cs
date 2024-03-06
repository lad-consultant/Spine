using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Abstractions;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Behaviors
{
    public class ActivityTrackingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ITrackableActivity
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly IMediator mediator;
        private readonly IAsyncRepository<Activity> activityRepository;
        private readonly ILogger<ActivityTrackingBehavior<TRequest, TResponse>> logger;

        public ActivityTrackingBehavior(
            AuthenticationStateProvider authenticationStateProvider,
            IMediator mediator,
            IAsyncRepository<Activity> activityRepository,
            ILogger<ActivityTrackingBehavior<TRequest, TResponse>> logger)
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.mediator = mediator;
            this.activityRepository = activityRepository;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle the activity tracking request.");

            var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();

            var user = authenticationState.User;

            if (user.Identity.IsAuthenticated)
            {
                await activityRepository.AddAsync(
                    new Activity() 
                    { 
                        UserId = user.Identity.Name,
                        Description = (request as ITrackableActivity).GetActivityDescription(),
                        Timestamp = DateTime.Now
                    });
            }
            
            return await next();
        }
    }
}
