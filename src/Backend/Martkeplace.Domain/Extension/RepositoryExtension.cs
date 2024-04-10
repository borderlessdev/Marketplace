using Microsoft.Extensions.Configuration;

namespace Marketplace.Domain.Extension;

public static class RepositoryExtension
{
    public static string GetDatabaseName(this IConfiguration configurationManager)
    {
        var databaseName = configurationManager.GetConnectionString("DbName");

        return databaseName;
    }
    
    public static string GetConnection(this IConfiguration configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("DefaultConnection");

        return connectionString;
    }

    public static string GetFullConnection(this IConfiguration configurationManager)
    {
        var connectionString = configurationManager.GetConnection();
        var databaseName = configurationManager.GetDatabaseName();

        return $"{connectionString}Database={databaseName}";
    }
}
