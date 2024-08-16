using MediatR;
using Domain.Books;
using Application.Data;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BookManagementAPI_Tests")]
namespace Application.Books.Create;

internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        Book book = new Book(request.Title, request.Author, request.ISBN, request.PublicationYear);

        _bookRepository.Add(book);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

