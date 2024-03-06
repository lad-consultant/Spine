namespace TheSpine.Core.Enums
{
    public enum SortDirection
    {
        None,
        Ascending,
        Descending
    }

    public static class SortDirectionExtensions
    {
        public static string GetSortString(this SortDirection direction)
        {
            switch(direction) 
            {
                case SortDirection.Descending:
                    return "DESC";
                default:
                    return "ASC";
            }
        }
    }
}
