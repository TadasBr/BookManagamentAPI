using Application.Data;
using Domain.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Get;

internal sealed class GetBookQueryHandler : IRequestHandler<GetBookQuery, Book>
{
    private readonly IApplicationDbContext _context;

    public GetBookQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        Book? book = await _context
            .Books
            .Where(b => b.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return book is null ? throw new BookNotFoundException(request.Id) : book;
    }
}
