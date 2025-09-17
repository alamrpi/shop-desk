using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace ShopDesk.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        // It scans the assembly for profiles
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register FluentValidation
        // It scans the assembly for any classes that inherit from AbstractValidator
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Register MediatR
        // It scans the assembly for handlers (IRequestHandler)
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}