using MediatR;
using TheSpine.Application.Constants;
using TheSpine.Application.CustomAttributes;

namespace TheSpine.Application.Features.Commands.Activities.Delete
{
    [Authorize(Roles.Moderator)]
    public class DeleteActivityCommand : IRequest<Response>
    {
        public DeleteActivityCommand(ActivityViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ActivityViewModel ViewModel { get; }
    }
}
