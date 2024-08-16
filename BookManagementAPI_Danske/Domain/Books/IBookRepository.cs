namespace Domain.Books;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    void Add(Book book);

    void Update(Book book);

    void Remove(Book book);

    Task<List<Book>> SearchAsync(string? title, string? author);
}