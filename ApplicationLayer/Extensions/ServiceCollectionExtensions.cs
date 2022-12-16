using ApplicationLayer.Services;
using ApplicationLayer.Services.Implementations;
using DataAccessLayer.Models.MessageSources;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ICreateEmployee, CreateEmployee>();
        collection.AddScoped<IAuthoriseService, AuthoriseService>();
        collection.AddScoped<IMessagesService, MessagesService>();
        collection.AddScoped<IReportService, ReportService>();
        collection.AddScoped<IMessageSourceService, MessageSourceService>();
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<IHandleMessage, HandleMessage>();
        return collection;
    }
}