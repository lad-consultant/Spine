using System.ComponentModel.DataAnnotations;

using TheSpine.Core.Common;

namespace TheSpine.Core
{
    public class Segment : IdentifiableEntity
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        //public IEnumerable<SegmentItem> Rows { get; set; }
    }
}
