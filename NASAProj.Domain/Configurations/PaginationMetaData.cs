namespace NASAProj.Domain.Configurations
{
    public class PaginationMetaData
    {
        public PaginationMetaData(int totalCount, int pageSize, int pageIndex)
        {
            CurrentPage = pageIndex;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public int CurrentPage { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
