using Domain.Books;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public Task<Book?> GetByIdAsync(int id)
    {
        return _context.Books.SingleOrDefaultAsync(b => b.Id == id);
    }

    public void Add(Book book)
    {
        _context.Books.Add(book);
    }

    public void Update(Book book)
    {
        _context.Books.Update(book);
    }

    public void Remove(Book book)
    {
        _context.Books.Remove(book);
    }

    public async Task<List<Book>> SearchAsync(string? title, string? author)
    {
        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(b => b.Title.Contains(title));
        }

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.Author.Contains(author));
        }

        return await query.ToListAsync();
    }
}
