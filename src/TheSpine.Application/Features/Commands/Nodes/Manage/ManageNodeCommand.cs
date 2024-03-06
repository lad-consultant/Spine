using TheSpine.Application.Abstractions;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Constants;

namespace TheSpine.Application.Features.Commands.Nodes.Manage
{
    [Authorize(Roles.Moderator)]
    public class ManageNodeCommand : ICommand<Response>
    {
        public NodeViewModel Model { get; set; }
        public bool IsEditing { get; set; } = false;

        public ManageNodeCommand(NodeViewModel model, bool isEditing = false)
        {
            Model = model;
            IsEditing = isEditing;
        }
    }
}
