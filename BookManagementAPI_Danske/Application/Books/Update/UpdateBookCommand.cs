using Application.Abstractions.Messaging;

namespace Application.Books.Update;

public record UpdateBookCommand(
    int Id, 
    string Title, 
    string Author, 
    string ISBN, 
    int PublicationYear) : ICommand;

public record UpdateBookRequest (
    string Title, 
    string Author, 
    string ISBN, 
    int PublicationYear);
