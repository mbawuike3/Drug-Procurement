namespace Drug_Procurement.Helper;

public interface IPagination
{
    PagedResult<T> GetPaginatedResult<T>(IEnumerable<T> response, int _pageNumber, int _pageSize) where T : class;
}

public class Pagination : IPagination
{
    public PagedResult<T> GetPaginatedResult<T>(IEnumerable<T> response, int _pageNumber, int _pageSize) where T : class
    {


        string yes = "Yes";
        string no = "No";
        int currentPage = _pageNumber == 0 ? 1 : _pageNumber;
        int pageSize = _pageSize == 0 ? 10 : _pageSize;
        int count = 0;
        int totalPages = 0;
        string nextPage = string.Empty;
        string previousPage = currentPage > 1 ? yes : no;
        List<T> results = new();
        if (response != null && response.Any())
        {
            count = response.Count();
            totalPages = (int)Math.Ceiling(count / (double)pageSize);
            nextPage = currentPage < totalPages ? yes : no;
            var sort = (pageSize * currentPage) - pageSize;
            results = response.Skip(sort).Take(pageSize).ToList();
        }
        return new PagedResult<T>
        {
            Results = results,
            CurrentPage = currentPage,
            RecordsPerPage = pageSize,
            TotalPages = totalPages,
            RecordCount = count,
            PreviousPage = previousPage,
            NextPage = nextPage,
            Succeeded = true,
            Message = "Data returned successfully"
        };
    }
}
public class PagedResult<T> : PagedResultBase where T : class
{
    public IList<T> Results { get; set; }

    public PagedResult()
    {
        Results = new List<T>();
    }
}
public abstract class PagedResultBase
{
    public int CurrentPage { get; set; }
    public int RecordsPerPage { get; set; }
    public int RecordCount { get; set; }
    public int TotalPages { get; set; }
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public string? PreviousPage { get; set; }
    public string? NextPage { get; set; }
}