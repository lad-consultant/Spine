using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

using TheSpine.AppLibrary.Components;
using TheSpine.AppLibrary.Components.NodeTreeView;
using TheSpine.Application.Constants;
using TheSpine.Application.Features.Commands.Nodes;
using TheSpine.Application.Features.Commands.SegmentItems;

namespace TheSpine.AppLibrary.Pages
{
    public partial class Home
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string? Slug { get; set; }

        [Parameter]
        public string? NodeId { get; set; }

        [Parameter]
        public string? SegmentId { get; set; }

        private NodeViewModel selectedNode;

        private bool isModerator = false;

        private List<SegmentItemViewModel> rows = new List<SegmentItemViewModel>(100);
        private TreeViewComponent? treeViewComponent;
        private SegmentItemsDataGrid? segmentItemsDataGrid;

        private List<BreadcrumbItem> breadcrumbs
        {
            get
            {
                if (selectedNode is not null)
                {
                    var asd = selectedNode.FullPath.Select(x => new BreadcrumbItem(x.Title, x.Slug, x.Id == selectedNode.Id)).ToList();
                    return asd;
                }

                return Enumerable.Empty<BreadcrumbItem>().ToList();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            isModerator = state.User.IsInRole(Roles.Moderator);

         

            base.OnInitializedAsync();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            var currentUri = new Uri(NavigationManager.Uri);
            var queryString = System.Web.HttpUtility.ParseQueryString(currentUri.Query);
            var segmentId = queryString["segment"];

            if (!string.IsNullOrEmpty(segmentId))
            {
                SegmentId = segmentId;
                if (treeViewComponent is not null)
                {
                    treeViewComponent.SegmentId = segmentId;
                }
            }

            if ((parameters.TryGetValue<string>(nameof(Slug).ToLower(), out var value) 
                || parameters.TryGetValue<string>(nameof(NodeId).ToLower(), out var _))
                && treeViewComponent != null)
            {
                await treeViewComponent.SetParametersAsync(parameters);
            }

            await base.SetParametersAsync(parameters);
        }

        private void SelectedNodeChangedEvent(NodeViewModel nodeViewModel) 
        {
            if (nodeViewModel != null)
            {
                selectedNode = nodeViewModel;
            }
        }
    }
}
