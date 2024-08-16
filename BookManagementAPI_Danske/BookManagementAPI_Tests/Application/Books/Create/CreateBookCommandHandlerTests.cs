using Moq;
using Xunit;
using Application.Books.Create;
using Domain.Books;
using Application.Data;

namespace Application.Tests.Books.Create;

public class CreateBookCommandHandlerTests
{
    private readonly CreateBookCommandHandler _handler;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new CreateBookCommandHandler(_bookRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldAddBookAndSaveChanges()
    {
        // Arrange
        var command = new CreateBookCommand("Title", "Author", "123-456-789", 2023);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Once);

        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldNotAddBook()
    {
        // Arrange
        var invalidCommand = new CreateBookCommand("", "Author", "123-456-789", 2023);

        // Act
        await _handler.Handle(invalidCommand, CancellationToken.None);

        // Assert
        _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Never);

        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_FuturePublicationYear_ShouldNotAddBook()
    {
        // Arrange
        var futureYearCommand = new CreateBookCommand("Title", "Author", "123-456-789", DateTime.Now.Year + 1);

        // Act
        await _handler.Handle(futureYearCommand, CancellationToken.None);

        // Assert
        _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Never);

        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
