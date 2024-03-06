using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheSpine.Application.Features.Commands.Segments;
using TheSpine.Application.Features.Commands.Segments.Manage;

namespace TheSpine.AppLibrary.Components.Dialogs
{
    public partial class ManageSegmentDialog
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public EventCallback<int> OnSaveEventCallback { get; set; }

        [Parameter]
        public SegmentViewModel SegmentViewModel { get; set; } = new SegmentViewModel();

        [Parameter]
        public bool IsEditing { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        private MudForm form;
        private SegmentViewModelValidator validator = new SegmentViewModelValidator();

        private async Task Submit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                var response = await Mediator.Send(new ManageSegmentCommand(SegmentViewModel, IsEditing));

                Snackbar.Add(response.Message, response.IsSuccess ? Severity.Success : Severity.Error);

                if (response != null)
                {
                    if (response.IsSuccess)
                    {
                        await OnSaveEventCallback.InvokeAsync(SegmentViewModel.Id);

                        MudDialog.Close(DialogResult.Ok(true));
                    }
                }
            }
        }

        void Cancel() => MudDialog.Cancel();
    }
}
