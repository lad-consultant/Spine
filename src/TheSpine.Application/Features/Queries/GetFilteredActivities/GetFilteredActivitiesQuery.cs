using MediatR;
using TheSpine.Application.Constants;
using TheSpine.Application.CustomAttributes;
using TheSpine.Application.Features.Commands.Activities;
using TheSpine.Core.Enums;

namespace TheSpine.Application.Features.Queries.GetFilteredActivities
{
    [Authorize(Roles.Moderator)]
    public class GetFilteredActivitiesQuery  : IRequest<IEnumerable<ActivityViewModel>>
    {
        public GetFilteredActivitiesQuery(string searchTerm, string sortColumn, SortDirection sortDirection)
        {
            SearchTerm = searchTerm;
            SortColumn = sortColumn;
            SortDirection = sortDirection;
        }

        public string SearchTerm { get; }
        public string SortColumn { get; }
        public SortDirection SortDirection { get; }
    }
}
