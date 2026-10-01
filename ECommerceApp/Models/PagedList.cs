namespace ECommerceApp.Models;

public class PagedList<T>
{
    public List<T> Items { get; set; } = new();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public PagedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = count;
        Items = items;
    }
}