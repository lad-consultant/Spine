using TheSpine.Application.Abstractions;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Constants;

namespace TheSpine.Application.Features.Commands.ItemDetailedInfo.Manage
{
	[Authorize(Roles.Moderator)]
	public class ManageItemDetailedInfoCommand : ICommand<Response>
	{
		public ItemDetailedInfoViewModel ViewModel { get; set; }
		public bool IsEditing { get; set; }

		public ManageItemDetailedInfoCommand(ItemDetailedInfoViewModel viewModel, bool isEditing)
		{
			ViewModel = viewModel;
			IsEditing = isEditing;
		}
	}
}
