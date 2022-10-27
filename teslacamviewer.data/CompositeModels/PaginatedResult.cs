using System.Collections.Generic;

namespace teslacamviewer.data.CompositeModels
{
    public class PaginatedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
