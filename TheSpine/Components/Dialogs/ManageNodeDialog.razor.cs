using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheSpine.Application.Features.Commands.Nodes;
using TheSpine.Application.Features.Commands.Nodes.Manage;

namespace TheSpine.AppLibrary.Components.Dialogs
{
    public partial class ManageNodeDialog
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public EventCallback OnSaveEventCallback { get; set; }

        [Parameter]
        public NodeViewModel Model { get; set; } = new();

        [Parameter]
        public bool IsEditing { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        private MudForm form;
        private NodeViewModelValidator validator = new();

        private async Task Submit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                var response = await Mediator.Send(new ManageNodeCommand(Model, IsEditing));

                Snackbar.Add(response.Message, response.IsSuccess ? Severity.Success : Severity.Error);

                if (response?.IsSuccess == true)
                {
                    await OnSaveEventCallback.InvokeAsync();

                    MudDialog.Close(DialogResult.Ok(true));
                }
            }
        }

        void Cancel() => MudDialog.Cancel();
    }
}
