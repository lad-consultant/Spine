namespace TheSpine.Application.Features.Commands.ItemDetailedInfo
{
	public class ItemDetailedInfoViewModel
	{
		public int Id { get; set; } = -1;
		public string HtmlIdentifier { get; set; }
        public int SegmentItemId { get; set; }
		public string Title { get; set; }
		public string HtmlContent { get; set; }
		public string TextContent { get; set; }
		public bool CanEdit { get; set; } = true;
		public bool IsEditState { get; set; }

		public ItemDetailedInfoViewModel() { }

		public ItemDetailedInfoViewModel(ItemDetailedInfoViewModel vmToCopy)
		{
			Id = vmToCopy.Id;
			SegmentItemId = vmToCopy.SegmentItemId;
			Title = vmToCopy.Title;
			HtmlContent = vmToCopy.HtmlContent;
			TextContent = vmToCopy.TextContent;
			IsEditState = vmToCopy.IsEditState;
		}
	}
}
