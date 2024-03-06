using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Features.Commands.Activities.Delete
{
    public class DeleteActivityHandler : IRequestHandler<DeleteActivityCommand, Response>
    {
        private readonly IAsyncRepository<Activity> repository;
        private readonly ILogger<DeleteActivityHandler> logger;

        public DeleteActivityHandler(
            IAsyncRepository<Activity> repository,
            ILogger<DeleteActivityHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<Response> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the delete activity command.");

            var response = new Response();
            var model = request.ViewModel.Adapt<Activity>();

            try
            {
                await repository.DeleteAsync(model);

                response.IsSuccess= true;
                response.Message = $"Activity {model.Description} deleted successfully";

                logger.LogInformation(response.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to delete activity {model.Description}.");

                response.IsSuccess = false;
                response.Errors.Add(string.Empty, new string[] { ex.Message });
            }

            return response;
        }
    }
}
