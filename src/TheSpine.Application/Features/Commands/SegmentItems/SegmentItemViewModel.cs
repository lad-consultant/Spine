using TheSpine.Application.Features.Commands.Segments;
using TheSpine.Core;
using TheSpine.Core.Enums;

namespace TheSpine.Application.Features.Commands.SegmentItems
{
    public class SegmentItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; } = 1;
        public EaseOfUse? EaseOfUse { get; set; }
        public string Licensing { get; set; }
        public string Differentiator { get; set; }
        public string Notes { get; set; }
        public SegmentViewModel Segment { get; set; }
        public int NodeId { get; set; }

        public SegmentItemViewModel() { }

        public SegmentItemViewModel(SegmentItemViewModel viewModelToCopy)
        {
            Id = viewModelToCopy.Id;
            Title = viewModelToCopy.Title;
            Rating = viewModelToCopy.Rating;
            EaseOfUse = viewModelToCopy.EaseOfUse;
            Licensing = viewModelToCopy.Licensing;
            Differentiator = viewModelToCopy.Differentiator;
            Notes = viewModelToCopy.Notes;
            Segment = viewModelToCopy.Segment;
            NodeId = viewModelToCopy.NodeId;
        }
    } 
}
