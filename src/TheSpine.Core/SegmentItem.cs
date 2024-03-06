using TheSpine.Core.Common;
using TheSpine.Core.Enums;

namespace TheSpine.Core
{
    public class SegmentItem : IdentifiableEntity
    {
        public string Title { get; set; }
        public int Rating { get; set; } = 1;
        public EaseOfUse EaseOfUse { get; set; }
        public string Licensing { get; set; }
        public string Differentiator { get; set; }
        public string Notes { get; set; }
        public int SegmentId { get; set; }
        public int NodeId { get; set; }

        public SegmentItem() { }
        public SegmentItem(SegmentItem segmentItemToCopy)
        {
            Id = segmentItemToCopy.Id;
            Differentiator = segmentItemToCopy.Differentiator;
            Licensing = segmentItemToCopy.Licensing;
            Notes = segmentItemToCopy.Notes;
            Rating = segmentItemToCopy.Rating;
            Title = segmentItemToCopy.Title;
        }
    }
}
