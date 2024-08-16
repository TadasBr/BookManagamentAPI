using FluentValidation;

namespace Application.Books.Create;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .WithMessage("Title is required");

        RuleFor(command => command.Author)
            .NotEmpty()
            .WithMessage("Author is required");

        RuleFor(command => command.ISBN)
            .NotEmpty()
            .WithMessage("ISBN is required.")
            .Matches(@"^\d{3}-\d{1,5}-\d{1,7}-\d{1,7}-\d{1}$")
            .WithMessage("Invalid ISBN format");         

        RuleFor(command => command.PublicationYear)
            .LessThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("Publication year cannot be in the future.");
    }
}