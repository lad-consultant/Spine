using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using TheSpine.Application.Features.Commands.Nodes;

namespace TheSpine.AppLibrary.Components.NodeTreeView
{
    public partial class TreeViewItemBody
    {
        [Parameter]
        public NodeViewModel Context { get; set; }

        [Parameter]
        public EventCallback<int?> OnAddEventCallback { get; set; }

        [Parameter]
        public EventCallback<NodeViewModel> OnEditEventCallback { get; set; }

        [Parameter]
        public EventCallback<NodeViewModel> OnDeleteEventCallback { get; set; }

        [Parameter]
        public EventCallback<bool> OnMouseDownCallback { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async Task AddNode(int id)
        {
            await OnAddEventCallback.InvokeAsync(id);

            StateHasChanged();
        }

        private async Task EditNode(NodeViewModel node)
        {
            await OnEditEventCallback.InvokeAsync(node);

            StateHasChanged();
        }

        private async Task DeleteNode(NodeViewModel node)
        {
            await OnDeleteEventCallback.InvokeAsync(node);

            StateHasChanged();
        }

        private async void OnMouseDown(MouseEventArgs args)
        {
            await OnMouseDownCallback.InvokeAsync(true);
        }
    }
}
