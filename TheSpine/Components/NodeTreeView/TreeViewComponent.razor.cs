using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheSpine.Application.Features.Commands.Nodes;
using TheSpine.Application.Features.Queries.GetNodes;

namespace TheSpine.AppLibrary.Components.NodeTreeView
{
    public partial class TreeViewComponent
    {
        [Parameter]
        public string Slug { get; set; }

        [Parameter]
        public string NodeId { get; set; }

        [Parameter]
        public string? SegmentId { get; set; }

        [Parameter]
        public EventCallback<NodeViewModel> SelectedNodeChangedEventCallback { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public IMediator? Mediator { get; set; }

        private GetNodesResponse? nodesResponse;
        private string Query { get; set; } = string.Empty;
        private MudTreeView<NodeViewModel>? mudTreeView;
        private NodeTreeViewItem? nodeTreeViewItem;
        private NodeViewModel selectedNode;
        private bool isLoading = true;
        private List<MudTreeViewItem<NodeViewModel>> mudTreeViewItems = new List<MudTreeViewItem<NodeViewModel>>(100);

        /// <summary>
        /// This flag exists to prevent second render which is initiated by NavigateTo execution in order to avoid double data fetch.
        /// </summary>
        private bool shouldRender = true;

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            shouldRender = true;

            if (!string.IsNullOrEmpty(NodeId) && int.TryParse(NodeId, out var nodeId))
            {
                var node = nodesResponse.NodesFlat.FirstOrDefault(x => x.Id == nodeId);
                if (node != null)
                {
                    var url = !string.IsNullOrEmpty(SegmentId) ? $"{node.Slug}?segment={SegmentId}" : node.Slug;
                    NavigationManager.NavigateTo(url);
                }
            }
            else if (parameters.TryGetValue<string>(nameof(Slug).ToLower(), out var value))
            {
                await UpdateSelectedNodeAsync(value);

                // Forbid the render in case node is already selected but Slug parameter is changed.
                // This is situation when node is selected and NavigateTo is executed afterwards which will set the Slug parameter.
                // In that situation we don't need another render which would further execute one more data fetch.
                if (selectedNode is not null)
                    shouldRender = false;
            }

            await base.SetParametersAsync(parameters);
        }

        protected override bool ShouldRender()
        {
            return shouldRender;
        }

        public void StateHasChangedEvent()
        {
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Init();

                if (!string.IsNullOrEmpty(NodeId) && int.TryParse(NodeId, out var nodeId))
                {
                    var selectedNode = nodesResponse.NodesFlat.FirstOrDefault(x => x.Id == nodeId);
                    if (selectedNode != null)
                    {
                        var url = !string.IsNullOrEmpty(SegmentId) ? $"{selectedNode.Slug}?segment={SegmentId}" : selectedNode.Slug;
                        NavigationManager.NavigateTo(url);
                    }
                }
                else if (!string.IsNullOrEmpty(Slug))
                {
                    await UpdateSelectedNodeAsync(Slug);
                }

                StateHasChanged();
            }

          
        }

        public async Task UpdateSelectedNodeAsync(string slug)
        {
            var selectedNode = SelectNodeById(nodesResponse.NodesHierarchy, slug ?? DetermineDefaultSlug());

            if (selectedNode != null)
            {
                this.selectedNode = selectedNode;

                var currentNode = this.selectedNode;

                while (currentNode.Parent != null)
                {
                    currentNode.Parent.IsExpanded = true;
                    Console.WriteLine($"Expanded: {currentNode.Parent.Title}");
                    currentNode = currentNode.Parent;
                }

                nodeTreeViewItem?.Init();

                SelectTreeViewItem(this.selectedNode);

                await mudTreeView.SelectedValueChanged.InvokeAsync(selectedNode);

                await SelectedNodeChangedEventCallback.InvokeAsync(selectedNode);
            }
        }
        
        private string DetermineDefaultSlug()
        {
            if (nodesResponse.NodesHierarchy.Any())
                return nodesResponse.NodesHierarchy.OrderBy(x => x.OrderingIndex).First().Slug;

            return string.Empty;
        }

        private NodeViewModel SelectNodeById(IEnumerable<NodeViewModel> nodes, string slug)
        {
            if (selectedNode?.Slug == slug) return selectedNode;

            foreach (var node in nodes)
            {
                if (node.Slug == slug)
                {
                    return node;
                }

                if (node.Data.NodesHierarchy.Any())
                {
                    var selectedNode = SelectNodeById(node.Data.NodesHierarchy, slug);
                    if (selectedNode is not null)
                    {
                        return selectedNode;
                    }
                }
            }

            return null;
        }

        private async Task OnTreeViewUpdatedEvent(bool arg)
        {
            mudTreeViewItems.Clear();
            await Init();

            await UpdateSelectedNodeAsync(GetValidSlug());
        }

        private string GetValidSlug()
        {
            if (nodesResponse.NodesFlat.Any(x => x.Slug == Slug))
                return Slug;

            NavigationManager?.NavigateTo(string.Empty);
            return string.Empty;
        }

        private async Task Init()
        {
            nodesResponse = await Mediator.Send(new GetNodesQuery());

            isLoading = false;

            StateHasChanged();

            nodeTreeViewItem?.Init();
        }

        private void OnTreeViewItemRenderedEvent(MudTreeViewItem<NodeViewModel> item)
        {
            if (item == null)
                return;

            if (mudTreeViewItems.All(x => x.Value.Id != item.Value.Id))
            {
                mudTreeViewItems.Add(item);
            }

            SelectTreeViewItem(this.selectedNode);
        }

        private void AddRoot()
        {
            nodeTreeViewItem?.AddNode(null);
        }

        private void SelectedNodeChangedAsync(NodeViewModel selectedNodeViewModel)
        {
            shouldRender = true;

            if (selectedNode != null && selectedNodeViewModel == null)
            {
                SelectTreeViewItem(selectedNode);
                return;
            }

            selectedNode = selectedNodeViewModel;

            if (selectedNode != null)
            {
                var url = !string.IsNullOrEmpty(SegmentId) ? $"{selectedNode.Slug}?segment={SegmentId}" : selectedNode.Slug;
                NavigationManager?.NavigateTo(url ?? string.Empty);
            }
        }

        public void SelectTreeViewItem(NodeViewModel selectedNodeViewModel)
        {
            if (selectedNode == null)
                return;

            var itemToSelect = mudTreeViewItems.FirstOrDefault(x => x.Value.Id == selectedNodeViewModel.Id);

            if (itemToSelect != null)
            {
                mudTreeView.Select(itemToSelect);
            }
        }

        public void NodeExpandedChanged(Tuple<NodeViewModel, bool> nodeExpanded)
        { 
            Stack<NodeViewModel> stack = new Stack<NodeViewModel>();

            foreach (var node in nodesResponse.NodesHierarchy)
            {
                stack.Push(node);
            }

            while (stack.Count > 0) 
            {
                var current = stack.Pop();

                if (current.Id == nodeExpanded.Item1.Id)
                {
                    current.IsExpanded = nodeExpanded.Item2;
                    StateHasChanged();
                    break;
                }

                foreach (var child in current.Data.NodesHierarchy)
                {
                    stack.Push(child);
                }
            }
        }

        private bool IsMatch(NodeViewModel node)
        {
            var isMatch = false;

            foreach (var childNode in node.Data.NodesHierarchy)
            {
                if (IsMatch(childNode))
                {
                    isMatch = true;
                }
            }

            if (isMatch || node.Title.Contains(Query, StringComparison.OrdinalIgnoreCase))
            {
                node.IsExpanded = node.IsVisible = true;
                isMatch = true;
            }

            return isMatch;
        }
    }
}
