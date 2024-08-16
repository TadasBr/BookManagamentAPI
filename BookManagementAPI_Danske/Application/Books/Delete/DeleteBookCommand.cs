using MediatR;

namespace Application.Books.Delete;

public record DeleteBookCommand(int Id) : IRequest;

