using MediatR;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Constants;

namespace TheSpine.Application.Features.Commands.SegmentItems.DeleteSegmentItem
{
    [Authorize(Roles.Moderator)]
    public class DeleteSegmentItemCommand : IRequest<Response>
    {
        public DeleteSegmentItemCommand(SegmentItemViewModel segmentItemToDelete)
        {
            SegmentItemToDelete = segmentItemToDelete;
        }

        public SegmentItemViewModel SegmentItemToDelete { get; }
    }
}
