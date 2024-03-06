using TheSpine.Core.Common;

namespace TheSpine.Core
{
    public class Node : IdentifiableEntity
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public int OrderingIndex { get; set; }
    }
}
