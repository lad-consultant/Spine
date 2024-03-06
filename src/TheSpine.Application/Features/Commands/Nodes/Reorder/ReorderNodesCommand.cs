using TheSpine.Application.Abstractions;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Constants;

namespace TheSpine.Application.Features.Commands.Nodes.Reorder
{
    [Authorize(Roles.Moderator)]
    public class ReorderNodesCommand : ICommand<Response>
    {
        public ReorderNodesCommand(List<NodeViewModel> viewModels)
        {
            ViewModels = viewModels;
        }

        public List<NodeViewModel> ViewModels { get; }
    }
}
