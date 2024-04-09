using FluentMigrator.Runner;
using Marketplace.Domain.Repository;
using Marketplace.Infrastructure.RepositoryAccess;
using Martkeplace.Domain.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Marketplace.Infrastructurel;

public static class Bootstraper
{
    public static void AddingRepository(this IServiceCollection services, IConfiguration configuration)
    {
        AddingFluentMigrator(services, configuration);
    }

    private static void AddingFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var fullConnection = configuration.GetFullConnection();

        services.AddFluentMigratorCore().ConfigureRunner(c =>
        c.AddMySql5()
        .WithGlobalConnectionString(fullConnection)
        .ScanIn(Assembly.Load("Marketplace.Infrastructure")).For.All());
    }

    private static void AddingWorkUnity(IServiceCollection services)
    {
        services.AddScoped<IWorkUnity, WorkUnity>();
    }



}
