using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheSpine.Application.Features.Queries.GetActivities;
using TheSpine.Application.Features.Commands.Activities;
using TheSpine.Application.Features.Queries.GetActivitiesCount;
using TheSpine.Core;
using TheSpine.Application.Features.Queries.GetFilteredActivities;

namespace TheSpine.AppLibrary.Pages
{
    public partial class ActivitiesOverview
    {
        [Inject]
        public IMediator Mediator { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }

        private IEnumerable<ActivityViewModel> activities;

        private Dictionary<string, IEnumerable<ActivityViewModel>> searchCache = new Dictionary<string, IEnumerable<ActivityViewModel>>(100);

        private int totalItems;
        private bool isInitialized;

        private readonly string UserIdSortLabel = nameof(Activity.UserId);
        private readonly string DescriptionSortLabel = nameof(Activity.Description);
        private readonly string TimestampSortLabel = nameof(Activity.Timestamp);

        private string searchTerm = null;
        private MudTable<ActivityViewModel> table;

        private async Task<TableData<ActivityViewModel>> LoadData(TableState state)
        {
            Core.Enums.SortDirection direction = Core.Enums.SortDirection.None;

            if (state.SortDirection == SortDirection.Ascending)
            {
                direction = Core.Enums.SortDirection.Ascending;
            }
            else if (state.SortDirection == SortDirection.Descending)
            {
                direction = Core.Enums.SortDirection.Descending;
            }

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Non-search scenario will fetch the data from DB page by page.
                activities = await Mediator.Send(new GetActivitiesQuery(state.PageSize, state.Page, state.SortLabel, direction));
                totalItems = await Mediator.Send(new GetActivitiesCountQuery());

                return new TableData<ActivityViewModel>() { TotalItems = totalItems, Items = activities };
            }
            else
            {
                // Search scenario will fetch all the data at once and it will be cached.
                // This cache mechanism perhaps should be implemented as a cache service in a way that it should be aware of activity changes and update the cash accordingly.
                if (!searchCache.TryGetValue(searchTerm.ToLowerInvariant(), out activities))
                {
                    activities = await Mediator.Send(new GetFilteredActivitiesQuery(searchTerm, state.SortLabel, direction));

                    searchCache[searchTerm.ToLowerInvariant()] = activities;
                }

                totalItems = activities.Count();

                var pagedData = activities.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

                return new TableData<ActivityViewModel>() { TotalItems = totalItems, Items = pagedData };
            }
        }

        private void OnSearch(string text)
        {
            searchTerm = text;
            table.ReloadServerData();
        }
    }
}
