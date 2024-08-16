using Application.Abstractions.Behaviors;
using Application.Books.Create;
using Application.Books.Update;
using Application.Data;
using Domain.Books;
using FluentValidation;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("BooksDatabase"));

        services.AddScoped<IApplicationDbContext>(sp => 
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp => 
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IBookRepository, BookRepository>();

        services.AddValidatorsFromAssemblyContaining<CreateBookCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateBookCommandValidator>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}

