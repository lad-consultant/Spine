using MediatR;
using TheSpine.Application.Abstractions;
using TheSpine.Application.Features.Commands.Nodes;
using TheSpine.Application.Features.Commands.SegmentItems;

namespace TheSpine.Application.Features.Queries.GetSegmentItemsForLeafNode
{
    public class GetSegmentItemsForLeafNodeQuery : IRequest<IEnumerable<SegmentItemViewModel>>, ITrackableActivity
    {
        public GetSegmentItemsForLeafNodeQuery(NodeViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public NodeViewModel ViewModel { get; }

        public string GetActivityDescription()
        {
            return $"Node {ViewModel.Title} clicked";
        }
    }
}
