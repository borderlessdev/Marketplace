using FluentMigrator.Runner;
using Marketplace.Domain.Repository;
using Marketplace.Infrastructure.RepositoryAccess;
using Marketplace.Domain.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Marketplace.Infrastructure.RepositoryAccess.Repository;
using Marketplace.Domain.Repository.User;

namespace Marketplace.Infrastructure;

public static class Bootstraper
{
    public static void AddingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddingFluentMigrator(services, configuration);
        AddContexto(services, configuration);
        AddingWorkUnity(services);
        AddingRepository(services, configuration);
    }

    private static void AddingRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
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

    private static void AddContexto(IServiceCollection services, IConfiguration configuration)
    {
        var fullConnection = configuration.GetFullConnection();
        var serverVersion = ServerVersion.AutoDetect(fullConnection);

        services.AddDbContext<MarketplaceContext>(options =>
        {
            options.UseMySql(fullConnection, serverVersion);
        });
    }

}
