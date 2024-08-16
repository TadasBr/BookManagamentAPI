using Application.Data;
using Domain.Books;
using MediatR;

namespace Application.Books.Update;

internal sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    IBookRepository _bookRepository;
    IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetByIdAsync(request.Id);

        if (book is null)
        {
            throw new BookNotFoundException(request.Id);
        }

        book.Update(
            request.Title,
            request.Author,
            request.ISBN,
            request.PublicationYear);

        _bookRepository.Update(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

