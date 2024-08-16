namespace Domain.Books;

public sealed class BookNotFoundException : Exception
{
    public BookNotFoundException(int id) 
        : base($"Book with id {id} was not found.")
    {
    }
}

