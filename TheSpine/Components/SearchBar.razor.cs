using MediatR;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.Collections.Generic;
using System.Text;

using TheSpine.Application.Features.Queries.GetQuickLinks;
using TheSpine.Application.Features.Queries.Search;

namespace TheSpine.AppLibrary.Components
{
    public partial class SearchBar
    {
        [Inject]
        private IMediator Mediator { get; set; }
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        private async Task<IEnumerable<SearchViewModel>> Search(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            return await Mediator.Send(new SearchQuery { Input = input });
        }

        private string? ToStringSearchItem(SearchViewModel searchViewModel)
        {
            if (searchViewModel == null)
            {
                return null;
            }

            return searchViewModel.Title;
        }

        public async Task OnValueChanged(SearchViewModel searchViewModel)
        {
            if (searchViewModel == null)
            {
                return;
            }

            NavigationManager.NavigateTo(searchViewModel.Url);
        }
    }

}
