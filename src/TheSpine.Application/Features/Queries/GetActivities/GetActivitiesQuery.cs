using MediatR;
using TheSpine.Application.Constants;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Features.Commands.Activities;
using TheSpine.Core.Enums;

namespace TheSpine.Application.Features.Queries.GetActivities
{
    [Authorize(Roles.Moderator)]
    public class GetActivitiesQuery : IRequest<IEnumerable<ActivityViewModel>>
    {
        public GetActivitiesQuery(int pageSize, int pageNumber, string sortColumn, SortDirection sortDirection)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            SortColumn = sortColumn;
            SortDirection = sortDirection;
        }

        public int PageSize { get; }
        public int PageNumber { get; }
        public string SortColumn { get; }
        public SortDirection SortDirection { get; }
    }
}
