namespace Domain.Books;

public class Book
{
    public Book() { }

    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }

    public int Id { get; private set; }
    
    public string? Title { get; private set; }

    public string? Author { get; private set; }

    public string? ISBN { get; private set; }

    public int PublicationYear { get; private set; }

    public void Update(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }

    public void SetId(int id) => Id = id;
}

