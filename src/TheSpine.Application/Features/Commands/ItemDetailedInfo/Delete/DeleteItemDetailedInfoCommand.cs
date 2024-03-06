using MediatR;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Constants;

namespace TheSpine.Application.Features.Commands.ItemDetailedInfo.Delete
{
	[Authorize(Roles.Moderator)]
	public class DeleteItemDetailedInfoCommand : IRequest<Response>
	{
		public ItemDetailedInfoViewModel ViewModel { get; set; }

		public DeleteItemDetailedInfoCommand(ItemDetailedInfoViewModel viewModel)
		{
			ViewModel = viewModel;
		}
	}
}
