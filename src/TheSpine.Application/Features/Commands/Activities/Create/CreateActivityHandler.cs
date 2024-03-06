using Mapster;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Abstractions;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Features.Commands.Activities.Create
{
    public class CreateActivityHandler : ICommandHandler<CreateActivityCommand, Response>
    {
        private readonly IAsyncRepository<Activity> repository;
        private readonly ILogger<CreateActivityHandler> logger;

        public CreateActivityHandler(
            IAsyncRepository<Activity> repository,
            ILogger<CreateActivityHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<Response> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the create activity command.");

            var response = new Response();
            var model = request.ViewModel.Adapt<Core.Activity>();

            try
            {
                await repository.AddAsync(model);

                response.IsSuccess = true;
                response.Message = $"Activity {model.Description} is created successfully";

                logger.LogInformation(response.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to create the activity {model.Description}.");

                response.IsSuccess = false;
                response.Errors.Add(string.Empty, new string[] { ex.Message });
            }

            return response;
        }
    }
}
