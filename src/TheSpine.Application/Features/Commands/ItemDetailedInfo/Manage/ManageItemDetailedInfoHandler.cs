using Mapster;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Abstractions;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Commands.ItemDetailedInfo.Manage
{
	public class ManageItemDetailedInfoHandler : ICommandHandler<ManageItemDetailedInfoCommand, Response>
	{
		private readonly IItemDetailedInfoAsyncRepository repository;
        private readonly ILogger<ManageItemDetailedInfoHandler> logger;

        public ManageItemDetailedInfoHandler(
			IItemDetailedInfoAsyncRepository repository,
			ILogger<ManageItemDetailedInfoHandler> logger)
		{
			this.repository = repository;
            this.logger = logger;
        }

		public async Task<Response> Handle(ManageItemDetailedInfoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the manage item info command.");

            var response = new Response();
			var model = request.ViewModel;
			var detailedInfo = model.Adapt<Core.ItemDetailedInfo>();

			try
			{
				if (request.IsEditing)
				{
					await repository.UpdateAsync(detailedInfo);
				}
				else
				{
					await repository.AddAsync(detailedInfo);
				}

				response.IsSuccess = true;
				response.Message = request.IsEditing ? $"Item detailed information {model.Title} updated successfully" : $"Item detailed information {model.Title} created successfully";

                logger.LogInformation("Manage item info command succeeded.");
            }
			catch (Exception ex)
            {
                logger.LogError(ex, "Failed to manage item info.");

                response.IsSuccess = false;
				response.Errors.Add(string.Empty, new string[] { ex.Message });
			}

			return response;
		}
	}
}
