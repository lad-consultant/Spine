using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Features.Commands.ItemDetailedInfo;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Queries.GetItemDetailedInfos
{
	public class GetItemDetailedInfosHandler : IRequestHandler<GetItemDetailedInfosQuery, IEnumerable<ItemDetailedInfoViewModel>>
	{
		private readonly IItemDetailedInfoAsyncRepository repository;
        private readonly ILogger<GetItemDetailedInfosHandler> logger;

        public GetItemDetailedInfosHandler(
			IItemDetailedInfoAsyncRepository repository,
			ILogger<GetItemDetailedInfosHandler> logger)
		{
			this.repository = repository;
            this.logger = logger;
        }

		public async Task<IEnumerable<ItemDetailedInfoViewModel>> Handle(GetItemDetailedInfosQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the get item infos query.");

            var infos = await repository.GetBySegmentItemId(request.ViewModel.Id);

            logger.LogInformation("Get item infos query finished.");

            return infos.Adapt<IEnumerable<ItemDetailedInfoViewModel>>();
		}
	}
}
