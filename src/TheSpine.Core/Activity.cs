using TheSpine.Core.Common;

namespace TheSpine.Core
{
    public class Activity : IdentifiableEntity
    {
        public string UserId { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
