using MediatR;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using TheSpine.AppLibrary.Components.Dialogs;
using TheSpine.Application.Features.Commands.Nodes;
using TheSpine.Application.Features.Commands.SegmentItems;
using TheSpine.Application.Features.Commands.SegmentItems.DeleteSegmentItem;
using TheSpine.Application.Features.Queries.GetSegmentItemsForLeafNode;

namespace TheSpine.AppLibrary.Components
{
    public partial class SegmentItemsDataGrid
    {
        [Parameter]
        public NodeViewModel LeafNode { get; set; }
        [Parameter]
        public string? SelectedItemId { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private bool isLoading = true;
        private IEnumerable<SegmentItemViewModel> rows = new List<SegmentItemViewModel>(30);

        private DialogOptions closeOnEscapeKey = new()
        {
            CloseOnEscapeKey = true,
            DisableBackdropClick = true,
            FullWidth = true,
            MaxWidth = MaxWidth.Medium
        };

        private TableGroupDefinition<SegmentItemViewModel> groupDefinition = new()
        {
            GroupName = "SegmentTitle",
            Indentation = false,
            Expandable = false,
            Selector = (e) =>
            {
                if (e.Segment != null)
                {
                    return e.Segment.Title;
                }

                return "Uncategorized";
            }
        };

        protected override async Task OnParametersSetAsync()
        {
            isLoading = true;

            if (LeafNode != null)
            {
                rows = await Mediator.Send(new GetSegmentItemsForLeafNodeQuery(LeafNode));

                var currentUri = new Uri(NavigationManager.Uri);
                var queryString = System.Web.HttpUtility.ParseQueryString(currentUri.Query);
                var segmentId = queryString["segment"];

                if (!string.IsNullOrEmpty(segmentId) && int.TryParse(segmentId, out var value))
                {
                    SelectItem(value);
                }
            }

            isLoading = false;
            await base.OnParametersSetAsync();
        }

        private void AddNewSegmentItem()
        {
            DialogParameters parameters = new DialogParameters
            {
                { nameof(ManageSegmentItemDialog.SegmentItemViewModel), new SegmentItemViewModel() { NodeId = LeafNode.Id } },
                { nameof(ManageSegmentItemDialog.OnChangedEventCallback), new EventCallbackFactory().Create<int>(this, OnChangeEventCallbackEventHandler) }
            };

            DialogService.Show<ManageSegmentItemDialog>("Add new item", parameters, closeOnEscapeKey);
        }

        private void ManageCategories()
        {
            DialogParameters parameters = new DialogParameters
            {
                { nameof(SegmentsDialog.OnCloseEventCallback), new EventCallbackFactory().Create<int>(this, OnChangeEventCallbackEventHandler) }
            };

            DialogService.Show<SegmentsDialog>("Manage categories", parameters, closeOnEscapeKey);
        }

        private async void OnChangeEventCallbackEventHandler(int args)
        {
            rows = await Mediator.Send(new GetSegmentItemsForLeafNodeQuery(LeafNode));
            StateHasChanged();
        }

        private void Edit(SegmentItemViewModel segmentItemViewModel)
        {
            DialogParameters parameters = new DialogParameters
            {
                { nameof(ManageSegmentItemDialog.SegmentItemViewModel), segmentItemViewModel },
                { nameof(ManageSegmentItemDialog.IsEditing), true },
                { nameof(ManageSegmentItemDialog.OnChangedEventCallback), new EventCallbackFactory().Create<int>(this, OnChangeEventCallbackEventHandler) }
            };

            DialogService.Show<ManageSegmentItemDialog>($"Edit {segmentItemViewModel.Title}", parameters, closeOnEscapeKey);
        }

        private async Task Delete(SegmentItemViewModel segmentItemViewModel)
        {
            var result = await DialogService.ShowMessageBox("Warning",
                                                           $"Are you sure you want to delete {segmentItemViewModel.Title}? This action will also delete all related quick links to this item.",
                                                           "Delete", "Cancel");
            if (result != null && result.Value)
            {
                var response = await Mediator.Send(new DeleteSegmentItemCommand(segmentItemViewModel));

                if (response.IsSuccess)
                {
                    Snackbar.Add($"Successfully deleted {segmentItemViewModel.Title}", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Failed to delete the {segmentItemViewModel.Title}", Severity.Error);
                }

                rows = await Mediator.Send(new GetSegmentItemsForLeafNodeQuery(LeafNode));

                StateHasChanged();
            }
        }

        public void SelectItem(int id)
        {
            var segmentItemViewModel = rows.FirstOrDefault(x => x.Id == id);
            if (segmentItemViewModel is not null)
            {
                OnSelectedItem(segmentItemViewModel);
                StateHasChanged();
            }
        }

        private void OnSelectedItem(SegmentItemViewModel viewModel)
        {
            DialogParameters parameters = new DialogParameters
            {
                { nameof(ManageItemDetailedInfosDialog.ViewModel), viewModel },
                { nameof(ManageItemDetailedInfosDialog.Path), LeafNode.FullPath.Select(x => x.Title) }
            };

            DialogService.Show<ManageItemDetailedInfosDialog>(string.Empty,
                                                parameters,
                                                options: new()
                                                {
                                                    CloseOnEscapeKey = false,
                                                    DisableBackdropClick = true,
                                                    MaxWidth = MaxWidth.Large,
                                                    FullWidth = true
                                                });
        }
    }
}
