using Domain.Books;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

