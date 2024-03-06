using MediatR;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Constants;

namespace TheSpine.Application.Features.Commands.Segments.Delete
{
    [Authorize(Roles.Moderator)]
    public class DeleteSegmentCommand : IRequest<Response>
    {
        public DeleteSegmentCommand(SegmentViewModel segmentToDelete)
        {
            SegmentToDelete = segmentToDelete;
        }

        public SegmentViewModel SegmentToDelete { get; }
    }
}
