using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Utilities;
using TheSpine.AppLibrary.Components.Dialogs;
using TheSpine.Application.Constants;
using TheSpine.Application.Features.Commands.Nodes;
using TheSpine.Application.Features.Commands.Nodes.Delete;
using TheSpine.Application.Features.Commands.Nodes.Reorder;
using TheSpine.Application.Features.Commands.SegmentItems;
using TheSpine.Application.Features.Queries.GetNodes;
using TheSpine.Application.Features.Queries.GetSegmentItemsForLeafNode;

namespace TheSpine.AppLibrary.Pages
{
    public partial class Badges
    {
        [Inject]
        public IMediator Mediator { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }

        private List<SegmentItemViewModel> rows = new List<SegmentItemViewModel>(100);
        private GetNodesResponse nodesResponse;
        private List<NodeViewModel> ParentNodes = new List<NodeViewModel>(20);
        private List<NodeViewModel> LeafNodes { get; set; } = new(20);
        private MudChipSet? mudChipSetLeaf;
        private MudChip? addNodeChip;
        private MudChip? addLeafNodeChip;
        private MudDropContainer<NodeViewModel>? nodeDropContainer;
        private MudDropContainer<NodeViewModel>? leafDropContainer;

        private int selectedNodeId;
        private NodeViewModel selectedLeafNode;
        private bool isLoading;
        private bool isModerator;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;

            if (authenticationState is not null)
            {
                var authState = await authenticationState;
                var user = authState?.User;

                if (user?.Identity is not null && user.Identity.IsAuthenticated && user.IsInRole(Roles.Moderator))
                {
                    isModerator = true;
                }
            }
        }

        protected async override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await Init();
            }
        }

        private async Task Init()
        {
            nodesResponse = await Mediator.Send(new GetNodesQuery());

            ParentNodes = nodesResponse.NodesHierarchy.Where(n => n.ParentId == null).ToList();

            if (selectedNodeId > 0)
            {
                LeafNodes = nodesResponse.NodesHierarchy.Where(n => n.ParentId == selectedNodeId).ToList();
            }

            RefreshParentNodes();
            RefreshLeafNodes();

            isLoading = false;
            StateHasChanged();
        }

        private void SelectedChipChangedEventHandler(MudChip selectedMudChip)
        {
            rows.Clear();
            if (selectedMudChip == null) return;

            var node = selectedMudChip.Value as NodeViewModel;
            if (node != null)
            {
                selectedNodeId = node.Id;
                LeafNodes = nodesResponse.NodesHierarchy.Where(n => n.ParentId == node.Id).ToList();

                RefreshLeafNodes();
            }

            ClearLeafNodeSelection();
        }

        private async Task SelectedLeafChipChangedEventHandler(MudChip selectedMudChip)
        {
            rows.Clear();
            if (selectedMudChip == null) return;

            var selectedLeafNode = selectedMudChip.Value as NodeViewModel;
            if (selectedLeafNode != null)
            {
                this.selectedLeafNode = selectedLeafNode;
                rows = (await Mediator.Send(new GetSegmentItemsForLeafNodeQuery(selectedLeafNode))).ToList();
            }
        }

        private async Task DeleteNode(NodeViewModel model)
        {
            var result = await DialogService.ShowMessageBox("Warning",
                                                             $"Are you sure you want to delete {model.Title} node?",
                                                             "Delete", "Cancel");
            if (result != null && result.Value)
            {
                var response = await Mediator.Send(new DeleteNodeCommand(model));
                if (response.IsSuccess)
                {
                    Snackbar.Add(response.Message, Severity.Success);
                    ClearLeafNodeSelection();

                    await Init();

                    if (model.ParentId == null)
                    {
                        RefreshParentNodes();
                        LeafNodes.Clear();
                    }
                    else
                    {
                        RefreshLeafNodes();
                    }
                }
                else
                {
                    Snackbar.Add(response.Message, Severity.Error);
                }
            }
        }

        private async Task AddNode(int? parentId = null)
        {
            await ManageNode(false, new NodeViewModel { ParentId = parentId });
        }

        private async Task EditNode(NodeViewModel model)
        {
            await ManageNode(true, model);
        }

        private async Task ManageNode(bool isEditing, NodeViewModel model)
        {
            DialogParameters parameters = new DialogParameters
            {
                { nameof(ManageNodeDialog.Model), model },
                { nameof(ManageNodeDialog.IsEditing), isEditing },
                { nameof(ManageNodeDialog.OnSaveEventCallback), EventCallback.Factory.Create(this, Init) }
            };

            DialogService.Show<ManageNodeDialog>(isEditing ? $"Edit {model.Title} node" : "Add new node",
                                                parameters,
                                                options: new()
                                                {
                                                    CloseOnEscapeKey = false,
                                                    DisableBackdropClick = true,
                                                    MaxWidth = MaxWidth.Medium,
                                                    FullWidth = true
                                                });
        }

        private bool IsDragAndDropDisabled(object obj)
        {
            return !isModerator;
        }

        private async Task NodeDropped(MudItemDropInfo<NodeViewModel> nodeViewModel)
        {
            ParentNodes.UpdateOrder(nodeViewModel, item => item.OrderingIndex);

            await Mediator.Send(new ReorderNodesCommand(ParentNodes));
        }

        private async Task LeafDropped(MudItemDropInfo<NodeViewModel> nodeViewModel)
        {
            LeafNodes.UpdateOrder(nodeViewModel, item => item.OrderingIndex);

            await Mediator.Send(new ReorderNodesCommand(LeafNodes));
        }

        private void RefreshParentNodes()
        {
            if(nodeDropContainer == null) return;

            // WA - According to documentation this is needed in case collection has changed.
            nodeDropContainer.Items = ParentNodes;
            nodeDropContainer?.Refresh();

            StateHasChanged();
        }

        private void RefreshLeafNodes()
        {
            if(leafDropContainer == null) return;

            // WA - According to documentation this is needed in case collection has changed.
            leafDropContainer.Items = LeafNodes;
            leafDropContainer?.Refresh();

            StateHasChanged();
        }

        private void ClearLeafNodeSelection()
        {
            if (mudChipSetLeaf != null)
            {
                // TODO: Issue with SelectedChipChangedEventHandler not being triggered should be fixed.
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                mudChipSetLeaf.SelectedChip = null;
                mudChipSetLeaf.SelectedChips = new MudChip[0];
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
                mudChipSetLeaf.SelectedValues.Clear();
            }
        }
    }
}
