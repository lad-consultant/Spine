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
using TheSpine.Application.Features.Queries.GetNodes;

namespace TheSpine.AppLibrary.Components.NodeTreeView
{
    public partial class NodeTreeViewItem
    {
		[Parameter]
		public GetNodesResponse NodesResponse { get; set; }

		[Parameter]
		public string DropZone { get; set; }

        [Parameter]
        public int? Id { get; set; }

        [Parameter]
        public NodeTreeViewItem Parent { get; set; }

        [Parameter]
        public EventCallback<bool> OnTreeViewUpdatedEventCallback { get; set; }

        [Parameter]
        public EventCallback<MudTreeViewItem<NodeViewModel>> OnTreeViewItemRenderedEventCallback { get; set; }

        [Parameter]
        public EventCallback<Tuple<NodeViewModel, bool>> OnTreeViewItemExpandedChangedEventCallback { get; set; }

		[Inject]
		public IMediator Mediator { get; set; }

		[Inject]
		public ISnackbar Snackbar { get; set; }

		[Inject]
		public IDialogService DialogService { get; set; }

		[CascadingParameter]
		private Task<AuthenticationState>? authenticationState { get; set; }

        private MudDropContainer<NodeViewModel>? nodeDropContainer;
        private MudTreeViewItem<NodeViewModel>? nodeTreeViewItem;

        public event Action StatusHasChangedEvent;
        public event Action<NodeViewModel> SelectionChangedEvent;

        private bool isModerator;

        /// <summary>
        /// Helper flag used as a WA for issue with unexpected drag & drop behavior.
        /// Nested drop zones are having troubles with detecting which item is actually being dragged so it detects multiple. 
        /// This flag prevents from reordering nodes which are not actually being dragged & dropped.
        /// </summary>
        private bool isItemClicked;

        protected override async Task OnInitializedAsync()
		{
			if (authenticationState is not null)
			{
				var authState = await authenticationState;
				var user = authState?.User;

				if (user?.Identity is not null && user.Identity.IsAuthenticated && user.IsInRole(Roles.Moderator))
				{
					isModerator = true;
				}
			}

            if (Parent != null)
            {
                Parent.StatusHasChangedEvent += Init;
            }
        }

        public void Init()
        {
            StateHasChanged();

            nodeDropContainer?.Refresh();

            StatusHasChangedEvent?.Invoke();

            StateHasChanged();
        }

        private bool CanDrop(NodeViewModel obj, string str)
		{
			if(obj == null || string.IsNullOrWhiteSpace(str) || !isItemClicked)
				return false;

			return true;
		}

        private void OnMouseDownHandler(bool arg)
        {
            isItemClicked = true;
        }

		private bool IsDragAndDropDisabled(object obj)
		{
			return !isModerator;
		}

		private async Task NodeDropped(MudItemDropInfo<NodeViewModel> nodeViewModel)
		{
			NodesResponse.NodesHierarchy.UpdateOrder(nodeViewModel, item => item.OrderingIndex);

			await Mediator.Send(new ReorderNodesCommand(NodesResponse.NodesHierarchy.ToList()));
            isItemClicked = false;
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

                    await Reload();

                    if(Parent != null)
                        await Parent.Reload();
                }
                else
                {
                    Snackbar.Add(response.Message, Severity.Error);
                }
            }
        }

        public async Task AddNode(int? parentId)
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
                { nameof(ManageNodeDialog.OnSaveEventCallback), EventCallback.Factory.Create(this, Reload) }
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

        public async Task Reload()
        {
            NodesResponse = await Mediator.Send(new GetNodesQuery(Id));

            await OnTreeViewUpdatedEventCallback.InvokeAsync(true);

            Init();
        }

        public async Task OnTreeViewUpdatedEvent(bool arg)
        {
            await OnTreeViewUpdatedEventCallback.InvokeAsync(arg);
        }

        /// <summary>
        /// Used to populate the list of mud tree view items in order to be able to get the item objects to select.
        /// </summary>
        /// <param name="item">Tree view item.</param>
        private async Task AddTreeViewItem(MudTreeViewItem<NodeViewModel> item)
        {
            await OnTreeViewItemRenderedEventCallback.InvokeAsync(item);
        }

        private async Task NodeExpandedChangedEvent(NodeViewModel node, bool arg)
        {
            await OnTreeViewItemExpandedChangedEventCallback.InvokeAsync(new Tuple<NodeViewModel, bool>(node, arg));
        }

        private async Task NodeExpandedChangedEventWrapper(Tuple<NodeViewModel, bool> node)
        {
            await NodeExpandedChangedEvent(node.Item1, node.Item2);
        }
    }
}