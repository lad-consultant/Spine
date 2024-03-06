
using TheSpine.Application.Features.Commands.Nodes;

namespace TheSpine.Application.Features.Queries.GetNodes
{
    public class GetNodesResponse
    {
        public List<NodeViewModel> NodesHierarchy { get; set; } = new List<NodeViewModel>(100);
        public IEnumerable<NodeViewModel> NodesFlat { get; set; }
    }
}
