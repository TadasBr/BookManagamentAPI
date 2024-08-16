using Domain.Books;
using MediatR;

namespace Application.Books.GetAll;

internal sealed class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetAllAsync();
    }
}
