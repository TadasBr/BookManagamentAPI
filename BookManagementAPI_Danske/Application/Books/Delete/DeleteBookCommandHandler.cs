using Application.Data;
using Domain.Books;
using MediatR;

namespace Application.Books.Delete;

internal sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetByIdAsync(request.Id);

        if (book is null)
        {
            throw new BookNotFoundException(request.Id);
        }

        _bookRepository.Remove(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

