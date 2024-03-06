using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TheSpine.Application.Behaviors;
using TheSpine.Application.Behaviors.Validation;

namespace TheSpine.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = AssemblyReference.Assembly;
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assembly)
                    .AddOpenBehavior(typeof(DelayBehavior<,>))
                    .AddOpenBehavior(typeof(AuthorizationPipelineBehavior<,>))
                    .AddOpenBehavior(typeof(ActivityTrackingBehavior<,>));
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
}
}
