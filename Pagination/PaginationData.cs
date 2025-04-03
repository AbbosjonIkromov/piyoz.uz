namespace piyoz.uz.Pagination
{
    public class PaginationData<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }   
        public int PageCount { get; set; }
    }
}
