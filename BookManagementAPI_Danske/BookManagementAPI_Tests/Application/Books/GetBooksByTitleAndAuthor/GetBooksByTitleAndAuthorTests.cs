using Application.Books.GetAll;
using Application.Books.GetBooksByTitleOrAuthor;
using Domain.Books;
using Moq;
using Xunit;

namespace Application.Tests.Books.GetBooksByTitleAndAuthor;

public class GetAllBooksByTitleOrAuthorQueryHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly GetAllBooksByTitleOrAuthorQueryHandler _handler;

    public GetAllBooksByTitleOrAuthorQueryHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new GetAllBooksByTitleOrAuthorQueryHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnBooks()
    {
        // Arrange
        var query = new GetBooksByTitleOrAuthorQuery("Title", "Author");
        var books = new List<Book>
        {
            new Book("Title1", "Author1", "123-456-789", 2020),
            new Book("Title2", "Author2", "987-654-321", 2021)
        };

        _bookRepositoryMock
            .Setup(repo => repo.SearchAsync("Title", "Author"))
            .ReturnsAsync(books);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Title1", result[0].Title);
        Assert.Equal("Author1", result[0].Author);
        Assert.Equal("Title2", result[1].Title);
        Assert.Equal("Author2", result[1].Author);
    }

    [Fact]
    public async Task Handle_NoBooksFound_ShouldReturnEmptyList()
    {
        // Arrange
        var query = new GetBooksByTitleOrAuthorQuery("Title", "Author");
        var books = new List<Book>();

        _bookRepositoryMock
            .Setup(repo => repo.SearchAsync("Title", "Author"))
            .ReturnsAsync(books);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ShouldThrowException()
    {
        // Arrange
        var query = new GetBooksByTitleOrAuthorQuery("Title", "Author");

        _bookRepositoryMock
            .Setup(repo => repo.SearchAsync("Title", "Author"))
            .ThrowsAsync(new System.Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<System.Exception>(() => _handler.Handle(query, CancellationToken.None));
    }
}
