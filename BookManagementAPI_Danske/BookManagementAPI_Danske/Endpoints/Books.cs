using Application.Books.Create;
using Application.Books.Delete;
using Application.Books.Get;
using Application.Books.GetAll;
using Application.Books.GetBooksByTitleOrAuthor;
using Application.Books.Update;
using Carter;
using Domain.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI_Danske.Endpoints;

public class Books : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var apiGroup = app.MapGroup("api");

        apiGroup.MapGet("books", async (ISender sender) =>
        {
            return Results.Ok(await sender.Send(new GetAllBooksQuery()));
        });

        apiGroup.MapGet("books/{id:int}", async (int id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetBookQuery(id)));
            }
            catch (BookNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        apiGroup.MapPost("books", async (CreateBookCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Created();
        });

        apiGroup.MapPut("books/{id:int}", async (int id, [FromBody] UpdateBookRequest request, ISender sender) =>
        {
            try 
            {
                UpdateBookCommand command = new UpdateBookCommand(
                    id,
                    request.Title,
                    request.Author,
                    request.ISBN,
                    request.PublicationYear);

                await sender.Send(command);

                return Results.NoContent();
            }
            catch (BookNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        apiGroup.MapDelete("books/{id:int}", async (int id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteBookCommand(id));

                return Results.NoContent();
            }
            catch (BookNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        apiGroup.MapGet("books/search", async (string? title, string? author, ISender sender) =>
        {
            var query = new GetBooksByTitleOrAuthorQuery(title, author);
            var result = await sender.Send(query);

            return Results.Ok(result);
        });
    }
}

