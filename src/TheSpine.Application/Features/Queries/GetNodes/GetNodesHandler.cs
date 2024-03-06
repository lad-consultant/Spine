using Mapster;

using MediatR;

using Microsoft.Extensions.Logging;

using TheSpine.Application.Features.Commands.Nodes;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Queries.GetNodes
{
    public class GetNodesHandler : IRequestHandler<GetNodesQuery, GetNodesResponse>
    {
        private readonly INodeAsyncRepository _repository;
        private readonly ILogger<GetNodesHandler> logger;

        public GetNodesHandler(
            INodeAsyncRepository repository,
            ILogger<GetNodesHandler> logger)
        {
            _repository = repository;
            this.logger = logger;
        }

        public async Task<GetNodesResponse> Handle(GetNodesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the get nodes query.");

            var nodes = await _repository.GetAllAsync();

            logger.LogInformation("Get nodes query finished.");

            // Create the hierarchy
            var nodeGroupsByParent = nodes.Adapt<IEnumerable<NodeViewModel>>().GroupBy(x => x.ParentId);

            // Flat node structure
            Dictionary<int, NodeViewModel> nodesDict = new Dictionary<int, NodeViewModel>();

            Stack<NodeViewModel> stack = new Stack<NodeViewModel>();

            foreach (var nodeGroup in nodeGroupsByParent)
            {
                if (nodeGroup.Key == request.Id)
                {
                    foreach (var node in nodeGroup)
                    {
                        // Initialize root nodes with its full path
                        node.FullPath.Add(node);
                        stack.Push(node);
                        nodesDict.Add(node.Id, node);
                    }
                }
                else
                {
                    foreach (var node in nodeGroup)
                    {
                        nodesDict.Add(node.Id, node);
                    }
                }
            }

            // Save the roots for return value
            var rootIds = stack.ToList().Select(x => x.Id);

            // Populate the children for each node using groupings.
            // Creating hierarchy using DFS.
            while (stack.Count > 0)
            {
                var current = stack.Pop();

                var currentChildren = nodeGroupsByParent.FirstOrDefault(x => x.Key == current.Id);
                var currentNode = nodesDict[current.Id];

                if (current.ParentId is not null && nodesDict.TryGetValue(current.ParentId.Value, out var parent))
                {
                    current.Parent = parent;
                    currentNode.Parent = parent;
                }

                if (currentChildren != null)
                {
                    foreach (var child in currentChildren.OrderBy(x => x.OrderingIndex))
                    {
                        stack.Push(child);
                        var childNode = nodesDict[child.Id];

                        childNode.FullPath.AddRange(currentNode.FullPath);
                        childNode.FullPath.Add(childNode);

                        currentNode.Data.NodesHierarchy.Add(childNode);
                        childNode.Parent = currentNode;
                    }
                }
            }

            return new GetNodesResponse
            {
                NodesHierarchy = rootIds.Select(x => nodesDict[x]).OrderBy(x => x.OrderingIndex).ToList(),
                NodesFlat = nodesDict.Values
            };
        }
    }
}