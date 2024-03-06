using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Abstractions;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Behaviors
{
    public class DelayBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ITrackableActivity
    {
       
        public DelayBehavior(
            AuthenticationStateProvider authenticationStateProvider,
            IMediator mediator,
            IAsyncRepository<Activity> activityRepository,
            ILogger<ActivityTrackingBehavior<TRequest, TResponse>> logger)
        {
            
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await Task.Delay(300);
            
            return await next();
        }
    }
}
