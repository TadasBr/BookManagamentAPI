using Application.Books.GetBooksByTitleOrAuthor;
using Domain.Books;
using MediatR;

namespace Application.Books.GetAll;

internal sealed class GetAllBooksByTitleOrAuthorQueryHandler : IRequestHandler<GetBooksByTitleOrAuthorQuery, List<Book>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksByTitleOrAuthorQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<Book>> Handle(GetBooksByTitleOrAuthorQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.SearchAsync(request.Title, request.Author);

        return books.ToList();
    }
}
