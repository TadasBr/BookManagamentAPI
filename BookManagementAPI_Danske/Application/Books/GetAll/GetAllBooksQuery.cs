using Domain.Books;
using MediatR;

namespace Application.Books.GetAll;

public record GetAllBooksQuery : IRequest<List<Book>>;