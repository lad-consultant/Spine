using TheSpine.Application.Abstractions;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Constants;

namespace TheSpine.Application.Features.Commands.SegmentItems.Manage
{
    [Authorize(Roles.Moderator)]
    public class ManageSegmentItemCommand : ICommand<Response>
    {
        public ManageSegmentItemCommand(SegmentItemViewModel segmentItemToAdd, bool isEditing)
        {
            SegmentItemViewModel = segmentItemToAdd;
            IsEditing = isEditing;
        }

        public SegmentItemViewModel SegmentItemViewModel { get; }
        public bool IsEditing { get; }
    }
}
