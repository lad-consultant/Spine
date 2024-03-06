using Azure.Core;
using Azure.Identity;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Kiota.Abstractions.Authentication;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TheSpine.Infrastructure.Authentication.Configuration;

namespace TheSpine.Infrastructure.DataAccess.Graph
{
    public class GraphService : IGraphService
    {
        private const string GRAPH_SCOPE = "https://graph.microsoft.com/.default";
        private readonly AuthenticationConfiguration _configuration;
        private readonly ILogger<GraphService> logger;

        public GraphService(
            IOptions<AuthenticationConfiguration> configuration,
            ILogger<GraphService> logger)
        {
            _configuration = configuration.Value;
            this.logger = logger;
        }

        public async Task<bool> IsModerator(string userId)
        {
            logger.LogInformation("Check if user is moderator.");

            try
            {
                var client = CreateClient();
                //get indicator if user is in moderator group
                var groups = await client.Users[userId].CheckMemberGroups.PostAsync(new()
                {
                    GroupIds = new List<string> { _configuration.ModeratorGroupId }
                });

                return groups?.Value?.Count == 1 && groups.Value[0] == _configuration.ModeratorGroupId;

            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return false;
            }
        }

        private GraphServiceClient CreateClient()
        {
            return new GraphServiceClient(CreateCredentials());
        }

        private TokenCredential CreateCredentials()
        {
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            return new ClientSecretCredential(_configuration.TenantId, 
                                                _configuration.ClientId, 
                                                _configuration.ClientSecret, 
                                                options);
        }
    }
}
