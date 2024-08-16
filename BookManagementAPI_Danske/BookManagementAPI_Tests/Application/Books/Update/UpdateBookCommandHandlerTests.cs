using Application.Books.Update;
using Application.Data;
using Domain.Books;
using Moq;
using Xunit;

namespace Application.Tests.Books.Update;

public class UpdateBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateBookCommandHandler _handler;

    public UpdateBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateBookCommandHandler(_bookRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateBookAndSaveChanges()
    {
        // Arrange
        var existingBook = new Book("Old Title", "Old Author", "123-456-789", 2020);
        existingBook.SetId(1);

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingBook);

        var command = new UpdateBookCommand(1, "New Title", "New Author", "123-456-789", 2021);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _bookRepositoryMock.Verify(repo => repo.Update(It.Is<Book>(b =>
            b.Title == "New Title" &&
            b.Author == "New Author" &&
            b.ISBN == "123-456-789" &&
            b.PublicationYear == 2021)), Times.Once);

        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_BookNotFound_ShouldThrowBookNotFoundException()
    {
        // Arrange
        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book?)null);

        var command = new UpdateBookCommand(1, "New Title", "New Author", "123-456-789", 2021);

        // Act & Assert
        await Assert.ThrowsAsync<BookNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ShouldNotCallSaveChanges()
    {
        // Arrange
        var existingBook = new Book("Old Title", "Old Author", "123-456-789", 2020);
        existingBook.SetId(1);

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingBook);

        _bookRepositoryMock.Setup(repo => repo.Update(It.IsAny<Book>())).Throws(new Exception("Repository error"));

        var command = new UpdateBookCommand(1, "New Title", "New Author", "123-456-789", 2021);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));

        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
