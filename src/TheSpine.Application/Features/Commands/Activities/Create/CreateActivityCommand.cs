using TheSpine.Application.Abstractions;

namespace TheSpine.Application.Features.Commands.Activities.Create
{
    public class CreateActivityCommand : ICommand<Response>
    {
        public CreateActivityCommand(ActivityViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ActivityViewModel ViewModel { get; }
    }
}
