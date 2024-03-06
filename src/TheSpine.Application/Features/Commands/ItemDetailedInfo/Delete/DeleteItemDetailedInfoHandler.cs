using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Commands.ItemDetailedInfo.Delete
{
	public class DeleteItemDetailedInfoHandler : IRequestHandler<DeleteItemDetailedInfoCommand, Response>
	{
		private readonly IItemDetailedInfoAsyncRepository repository;
        private readonly ILogger<DeleteItemDetailedInfoHandler> logger;

        public DeleteItemDetailedInfoHandler(
			IItemDetailedInfoAsyncRepository repository,
			ILogger<DeleteItemDetailedInfoHandler> logger)
		{
			this.repository = repository;
            this.logger = logger;
        }

		public async Task<Response> Handle(DeleteItemDetailedInfoCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Handling the delete item info command.");

			var response = new Response();
			var info = request.ViewModel.Adapt<Core.ItemDetailedInfo>();

			try
			{
				await repository.DeleteAsync(info);

				response.IsSuccess = true;
				response.Message = $"Item detailed information {info.Title} deleted successfully";

                logger.LogInformation("Delete item info command succeeded.");
            }
			catch (Exception ex)
			{
				logger.LogError(ex, "Failed to delete item info.");

				response.IsSuccess = false;
				response.Errors.Add(string.Empty, new string[] { ex.Message });
			}

			return response;
		}
	}
}
