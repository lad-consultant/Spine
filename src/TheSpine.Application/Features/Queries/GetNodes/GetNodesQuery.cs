using MediatR;

namespace TheSpine.Application.Features.Queries.GetNodes
{
    public class GetNodesQuery : IRequest<GetNodesResponse>
    {
        public GetNodesQuery(int? id = null)
        {
            Id = id;
        }

        public int? Id { get; }
    }
}