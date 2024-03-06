using TheSpine.Application.Features.Queries.GetNodes;
using TheSpine.Application.Helpers;

namespace TheSpine.Application.Features.Commands.Nodes
{
    public class NodeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug => SlugGenerator.GenerateSlug(string.Join('-', FullPath.Select(x => x.Title)));
        public int? ParentId { get; set; }
        public int OrderingIndex { get; set; }
        public bool IsExpanded { get; set; }
        public NodeViewModel Parent { get; set; }
        public GetNodesResponse Data { get; set; } = new GetNodesResponse();
        public List<NodeViewModel> FullPath { get; set; } = new(20);

        public bool IsVisible { get; set; } = true;
    }
}
