using TheSpine.Core.Common;

namespace TheSpine.Core
{
	public class ItemDetailedInfo : IdentifiableEntity
	{
		public int SegmentItemId { get; set; }
		public string HtmlContent { get; set; }
		public string TextContent { get; set; }
		public string Title { get; set; }
	}
}
