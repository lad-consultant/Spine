using Mapster;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Abstractions;
using TheSpine.Application.Features.Commands.Nodes.Manage;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Features.Commands.Nodes.Create
{
    public class ManageNodeCommandHandler : ICommandHandler<ManageNodeCommand, Response>
    {
        private readonly IAsyncRepository<Node> repository;
        private readonly ILogger<ManageNodeCommandHandler> logger;

        public ManageNodeCommandHandler(
            IAsyncRepository<Node> repository,
            ILogger<ManageNodeCommandHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<Response> Handle(ManageNodeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the manage node command.");

            var response = new Response();
            try
            {
                var model = request.Model;
                var node = request.Model.Adapt<Node>();

                if (request.IsEditing)
                {
                    await repository.UpdateAsync(node);
                }
                else
                {
                    await repository.AddAsync(node);
                }

                response.IsSuccess = true;
                response.Message = request.IsEditing ? $"Node {model.Title} updated successfully" : $"Node {model.Title} created successfully";

                logger.LogInformation("Manage node command succeeded.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to manage node.");

                response.IsSuccess = false;
                response.Errors.Add(string.Empty, new string[] { ex.Message });
            }

            return response;
        }
    }
}
