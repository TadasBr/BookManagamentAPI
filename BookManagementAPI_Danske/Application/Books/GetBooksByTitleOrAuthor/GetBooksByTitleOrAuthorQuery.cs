using Domain.Books;
using MediatR;

namespace Application.Books.GetBooksByTitleOrAuthor;

public record GetBooksByTitleOrAuthorQuery(string? Title, string? Author) : IRequest<List<Book>>;
