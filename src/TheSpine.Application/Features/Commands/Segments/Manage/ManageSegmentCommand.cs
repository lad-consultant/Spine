using TheSpine.Application.Abstractions;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Constants;

namespace TheSpine.Application.Features.Commands.Segments.Manage
{
    [Authorize(Roles.Moderator)]
    public class ManageSegmentCommand : ICommand<Response>
    {
        public ManageSegmentCommand(SegmentViewModel segmentViewModel, bool isEditing)
        {
            SegmentViewModel = segmentViewModel;
            IsEditing = isEditing;
        }

        public SegmentViewModel SegmentViewModel { get; }
        public bool IsEditing { get; }
    }
}
