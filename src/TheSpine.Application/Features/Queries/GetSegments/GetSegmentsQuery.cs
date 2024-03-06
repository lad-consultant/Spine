using MediatR;
using TheSpine.Application.Features.Commands.Segments;

namespace TheSpine.Application.Features.Queries.GetSegments
{
    public class GetSegmentsQuery : IRequest<IEnumerable<SegmentViewModel>>
    {
    }
}
