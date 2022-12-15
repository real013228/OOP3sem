using ApplicationLayer.Services;
using ApplicationLayer.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ICreateEmployee, CreateEmployee>();

        return collection;
    }
}