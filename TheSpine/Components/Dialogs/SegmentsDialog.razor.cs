using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheSpine.Application.Features.Commands.Segments;
using TheSpine.Application.Features.Commands.Segments.Delete;
using TheSpine.Application.Features.Queries.GetSegments;

namespace TheSpine.AppLibrary.Components.Dialogs
{
    public partial class SegmentsDialog
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public EventCallback<int> OnCloseEventCallback { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private bool loading = true;
        private IEnumerable<SegmentViewModel> segments = new List<SegmentViewModel>(30);

        private DialogOptions closeOnEscapeKey = new()
        {
            CloseOnEscapeKey = true,
            DisableBackdropClick = true,
            FullWidth = true,
            MaxWidth = MaxWidth.Medium
        };

        protected override async Task OnInitializedAsync()
        {
            segments = await Mediator.Send(new GetSegmentsQuery());
            loading = false;
            await base.OnInitializedAsync();
        }

        private void AddNewSegment()
        {
            DialogParameters parameters = new DialogParameters
            {
                { nameof(ManageSegmentDialog.IsEditing), false },
                { nameof(ManageSegmentDialog.OnSaveEventCallback), new EventCallbackFactory().Create<int>(this, OnChangeEventCallbackEventHandler) }
            };

            DialogService.Show<ManageSegmentDialog>($"Add new category", parameters, closeOnEscapeKey);
        }

        private void EditSegment(SegmentViewModel segmentViewModel)
        {
            DialogParameters parameters = new DialogParameters
            {
                { nameof(ManageSegmentDialog.SegmentViewModel), segmentViewModel },
                { nameof(ManageSegmentDialog.IsEditing), true },
                { nameof(ManageSegmentDialog.OnSaveEventCallback), new EventCallbackFactory().Create<int>(this, OnChangeEventCallbackEventHandler) }
            };

            DialogService.Show<ManageSegmentDialog>($"Edit {segmentViewModel.Title}", parameters, closeOnEscapeKey);
        }

        private async Task DeleteSegment(SegmentViewModel segmentViewModel)
        {
            var result = await DialogService.ShowMessageBox("Warning",
                                                           $"Are you sure you want to delete {segmentViewModel.Title}? This action will also delete all the items and quick links related to this category.",
                                                           "Delete", "Cancel");
            if (result != null && result.Value)
            {
                var response = await Mediator.Send(new DeleteSegmentCommand(segmentViewModel));

                if (response.IsSuccess)
                {
                    Snackbar.Add($"Successfully deleted {segmentViewModel.Title}", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Failed to delete the {segmentViewModel.Title}", Severity.Error);
                }

                // Update segments collection.
                segments = await Mediator.Send(new GetSegmentsQuery());

                StateHasChanged();
            }
        }

        private async void OnChangeEventCallbackEventHandler(int id)
        {
            // Update segments collection.
            segments = await Mediator.Send(new GetSegmentsQuery());

            StateHasChanged();
        }

        private async void Close()
        {
            await OnCloseEventCallback.InvokeAsync(1);
            MudDialog.Close();
        }
    }
}
