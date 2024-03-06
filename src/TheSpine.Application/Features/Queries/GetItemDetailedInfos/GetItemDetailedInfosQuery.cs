using MediatR;
using TheSpine.Application.Abstractions;
using TheSpine.Application.Features.Commands.ItemDetailedInfo;
using TheSpine.Application.Features.Commands.SegmentItems;

namespace TheSpine.Application.Features.Queries.GetItemDetailedInfos
{
	public class GetItemDetailedInfosQuery : IRequest<IEnumerable<ItemDetailedInfoViewModel>>, ITrackableActivity
	{
        public SegmentItemViewModel ViewModel { get; set; }

        public string GetActivityDescription()
        {
            return $"Item {ViewModel.Title} clicked.";
        }
    }
}
