namespace TheSpine.Infrastructure.Authentication.Configuration
{
    public class AuthenticationConfiguration
    {
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Domain { get; set; }
        public string CallbackPath { get; set; }
        public string Instance { get; set; }
        public string ModeratorGroupId { get; set; }
    }
}
