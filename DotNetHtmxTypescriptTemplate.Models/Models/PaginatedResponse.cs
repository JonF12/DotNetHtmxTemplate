namespace DotNetHtmxTypescriptTemplate.Models
{
    public class PaginatedResponse<T>
    {
        public required int TotalHits { get; set; }
        public required List<T> Hits { get; set; }
        public required int PageSize { get; set; }
        public required int StartFrom { get; set; }
        public int PageNumber => (StartFrom > 0 ? ((StartFrom - 1) / PageSize) + 1 : 0) + 1;
        public int MaxPage => (TotalHits > 0 ? (TotalHits - 1) / PageSize : 0) + 1;
        public int Next => (StartFrom + PageSize) < TotalHits ? (StartFrom + PageSize) : TotalHits;
        public int Prev => (StartFrom - PageSize) > 0 ? (StartFrom - PageSize) : 0;
        public bool CanGoPrevious => PageNumber > 1;
        public bool CanGoNext => PageNumber < MaxPage;
    }
}
