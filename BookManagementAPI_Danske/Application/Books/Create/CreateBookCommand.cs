using Application.Abstractions.Messaging;

namespace Application.Books.Create;

public record CreateBookCommand(
    string Title,   
    string Author,
    string ISBN,
    int PublicationYear) : ICommand;

