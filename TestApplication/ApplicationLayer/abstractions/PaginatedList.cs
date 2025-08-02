using Microsoft.EntityFrameworkCore;

namespace TestApplication.ApplicationLayer.abstractions;

public class PaginatedList<T>(List<T> items, int pageNumber,int count,int pageSize)
{
    public List<T> items { get; private set; }=items;
    public int pageNumber { get; private set; } = pageNumber;
    public int totalPages { get; private set; } = (int)Math.Ceiling(count / (double)pageSize);

    public bool hasPreviosPage => pageNumber > 1;
    public bool hasNextPage => pageNumber > totalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T>source,int pageNumber, int pageSize )
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, pageNumber, count, pageSize);
    }
}
