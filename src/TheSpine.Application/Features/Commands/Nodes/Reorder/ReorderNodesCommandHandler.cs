using Microsoft.Extensions.Logging;
using TheSpine.Application.Abstractions;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Commands.Nodes.Reorder
{
    public class ReorderNodesCommandHandler : ICommandHandler<ReorderNodesCommand, Response>
    {
        private readonly INodeAsyncRepository repository;
        private readonly ILogger<ReorderNodesCommandHandler> logger;

        public ReorderNodesCommandHandler(
            INodeAsyncRepository repository,
            ILogger<ReorderNodesCommandHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<Response> Handle(ReorderNodesCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the reorder nodes command.");

            Response response = new Response();

            try
            {
                await repository.ReorderNodes(request.ViewModels);

                response.IsSuccess = true;
                response.Message = "Nodes reordered successfully";

                logger.LogInformation("Reorder nodes command succeeded.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to reorder nodes.");

                response.IsSuccess = false;
                response.Errors.Add(string.Empty, new string[] {ex.Message });
            }

            return response;
        }
    }
}
