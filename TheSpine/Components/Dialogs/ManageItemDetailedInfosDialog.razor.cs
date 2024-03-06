using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using TheSpine.Application.Features.Commands.ItemDetailedInfo;
using TheSpine.Application.Features.Commands.SegmentItems;
using TheSpine.Application.Features.Queries.GetItemDetailedInfos;

namespace TheSpine.AppLibrary.Components.Dialogs
{
    public partial class ManageItemDetailedInfosDialog
	{
        private List<ItemDetailedInfoViewModel> infoViewModels = new List<ItemDetailedInfoViewModel>(10);

		private CancellationTokenSource cancelationTokenSource = new CancellationTokenSource();
        private bool loading = true;

        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public SegmentItemViewModel ViewModel { get; set; }

        [Parameter]
        public IEnumerable<string> Path { get; set; }

        private string path = string.Empty;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (infoViewModels.Any())
            {
                var lastVM = infoViewModels.Last();

                if (lastVM.Id == -1)
                {
                    await JSRuntime.InvokeVoidAsync("scrollToElement", lastVM.HtmlIdentifier);
                }
            }
        }

        protected override async Task OnInitializedAsync()
		{
			await InitializeData();

			loading = false;
        }

        private async Task OnChangeEventCallbackEventHandler(int args)
        {
            await InitializeData();

			StateHasChanged();
        }

        private async Task InitializeData()
        {
            path = string.Join(" - ", Path);
			infoViewModels = (await Mediator.Send(new GetItemDetailedInfosQuery { ViewModel = ViewModel }, cancelationTokenSource.Token)).ToList();

			foreach (var info in infoViewModels)
			{
				info.CanEdit = true;
			}
		}

        private void OnEnterEditStateEventHandler(int id)
        {
            // Disable all other info edit buttons.
			foreach (var info in infoViewModels)
			{
				if (info.Id != id)
				{
					info.CanEdit = false;
				}
			}

			StateHasChanged();
		}

		private bool IsAnyViewModelInEdit()
		{
            return infoViewModels.Any(vm => vm.IsEditState);
		}

		private async Task CreateNewInfoPlaceHolder()
        {
            foreach (var info in infoViewModels)
            {
                info.CanEdit = false;
            }

            var newViewModel = new ItemDetailedInfoViewModel() { IsEditState = true, SegmentItemId = ViewModel.Id, HtmlIdentifier = Guid.NewGuid().ToString() };
            infoViewModels.Add(newViewModel);
            
            StateHasChanged();
		}

        private void Close()
        {
            cancelationTokenSource.Cancel();
            MudDialog.Cancel();
        }
    }
}
