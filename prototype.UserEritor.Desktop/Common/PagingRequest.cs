namespace prototype.UserEritor.Desktop
{

    public class PagingRequest
    {
        public PagingRequest()
        {
            NavsCount = 5;
            PageIndex = 1;
            PageSize = 10;
        }
        public int NavsCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Offset => PageIndex < 1 ? 0 : (PageIndex - 1) * PageSize;
        public int? RecordIndex { get; set; }
        public int RecordPageIndex
        {
            get
            {
                if (!RecordIndex.HasValue)
                {
                    return PageIndex - 1;
                }
                return (RecordIndex.Value - 1) / PageSize + 1;
            }
        }
    }
}
