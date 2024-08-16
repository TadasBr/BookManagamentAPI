using Domain.Books;
using MediatR;

namespace Application.Books.Get;

public record GetBookQuery(int Id) : IRequest<Book>;