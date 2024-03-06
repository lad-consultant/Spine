using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheSpine.Application.Features.Commands.ItemDetailedInfo;
using TheSpine.Application.Features.Commands.ItemDetailedInfo.Delete;
using TheSpine.Application.Features.Commands.ItemDetailedInfo.Manage;

namespace TheSpine.AppLibrary.Components
{
    public partial class ItemDetailedInformation
	{
        [Parameter]
        public ItemDetailedInfoViewModel ViewModel { get; set; }

		[Parameter]
		public EventCallback<int> OnChangeEventCallback { get; set; }

		[Parameter]
		public EventCallback<int> OnEnterEditState { get; set; }

		[Inject]
		public IMediator Mediator { get; set; }

		[Inject]
		public ISnackbar Snackbar { get; set; }

		[Inject]
		public IDialogService DialogService { get; set; }

		private QuillEditor QuillHtml;
        private MudForm form;
        private ItemDetailedInfoViewModelValidator validator = new ItemDetailedInfoViewModelValidator();
		private string contentValidationMessage = string.Empty;


		private async Task OnEditClicked()
		{
			await OnEnterEditState.InvokeAsync(ViewModel.Id);

			ViewModel.IsEditState = true;

			StateHasChanged();
        }

        private bool IsTextContentValid()
        {
			if (string.IsNullOrWhiteSpace(ViewModel.TextContent))
			{
				contentValidationMessage = "Content must not be empty";
                return false;
			}

            return true;
		}

		private async Task OnDeleteClicked()
		{
			var result = await DialogService.ShowMessageBox("Warning",
															 $"Are you sure you want to delete {ViewModel.Title} item info?",
															 "Delete", "Cancel");
			if (result != null && result.Value)
			{
				var response = await Mediator.Send(new DeleteItemDetailedInfoCommand(ViewModel));
				Snackbar.Add(response.Message, response.IsSuccess ? Severity.Success : Severity.Error);

				await OnChangeEventCallback.InvokeAsync(1);

				StateHasChanged();
			}
		}

		private async Task OnConfirmChangesClicked()
		{
			ViewModel.TextContent = await QuillHtml.GetText();
			ViewModel.HtmlContent = await QuillHtml.GetHTML();

			await form.Validate();
			var isContentValid = IsTextContentValid();

			if (form.IsValid && isContentValid)
			{
				var response = await Mediator.Send(new ManageItemDetailedInfoCommand(ViewModel, ViewModel.Id != -1));

				if (response != null)
				{
					Snackbar.Add(response.Message, response.IsSuccess ? Severity.Success : Severity.Error);
					ViewModel.IsEditState = false;

					await OnChangeEventCallback.InvokeAsync(1);
				}
			}
		}

		private async Task OnCancelClicked() 
        {
			var result = await DialogService.ShowMessageBox("Warning",
															 $"Are you sure you want to stop editing?",
															 "Yes", "Cancel");
			if (result != null && result.Value)
			{
				ViewModel.IsEditState = false;

				// This will fetch fresh data from db.
				await OnChangeEventCallback.InvokeAsync(1);

				StateHasChanged();
			}
        }
    }
}
