using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Commands.Nodes.Delete
{
    internal class DeleteNodeCommandHandler : IRequestHandler<DeleteNodeCommand, Response>
    {
        private readonly INodeAsyncRepository _nodeRepository;
        private readonly ISegmentItemAsyncRepository segmentItemrepository;
        private readonly ILogger<DeleteNodeCommandHandler> logger;

        public DeleteNodeCommandHandler(
            INodeAsyncRepository nodeRepository,
            ISegmentItemAsyncRepository segmentItemrepository,
            ILogger<DeleteNodeCommandHandler> logger)
        {
            _nodeRepository = nodeRepository;
            this.segmentItemrepository = segmentItemrepository;
            this.logger = logger;
        }

        public async Task<Response> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the delete node command.");

            var response = new Response();

            try
            {
                // Collect all nodes for cascade deletion.
                List<int> idsToDelete = new List<int>(30);
                List<int> leafIds= new List<int>(30);

                Stack<NodeViewModel> stack = new Stack<NodeViewModel>();
                stack.Push(request.Model);

                while(stack.Count > 0) 
                {
                    var current = stack.Pop();

                    idsToDelete.Add(current.Id);

                    foreach (var nodeViewModel in current.Data.NodesHierarchy) 
                    {
                        stack.Push(nodeViewModel);
                    }

                    if (!current.Data.NodesHierarchy.Any())
                    { 
                        leafIds.Add(current.Id);
                    }
                }

                foreach (var leafId in leafIds)
                {
                    var segmentItemsToDelete = await segmentItemrepository.GetByLeafNodeIdAsync(leafId);

                    foreach(var segmentItem in segmentItemsToDelete)
                    {
                        await segmentItemrepository.DeleteIncludingRelations(segmentItem.Id);
                    }
                }

                await _nodeRepository.DeleteNodesWithIds(idsToDelete);

                response.IsSuccess = true;
                response.Message = $"Node {request.Model.Title} deleted successfully";

                logger.LogInformation("Delete node command succeeded.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete node.");

                response.IsSuccess = false;
                response.Errors.Add(string.Empty, new string[] { ex.Message });
            }

            return response;
        }
    }
}
