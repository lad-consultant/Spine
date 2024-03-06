using MediatR;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Constants;

namespace TheSpine.Application.Features.Commands.Nodes.Delete
{
    [Authorize(Roles.Moderator)]
    public class DeleteNodeCommand : IRequest<Response>
    {
        public NodeViewModel Model { get; set; }

        public DeleteNodeCommand(NodeViewModel model)
        {
            Model = model;
        }
    }
}
