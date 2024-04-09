using Dapper;
using MySqlConnector;

namespace Marketplace.Infrastructure.Migrations;

public static class Database
{
    public static void CreateDatabase(string connectionString, string dbName)
    {
        using var connection = new MySqlConnection(connectionString);
        
        var parameters = new DynamicParameters();
        parameters.Add("dbName", dbName);

        var reg = connection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @dbName", parameters);

        if (!reg.Any())
        {
            connection.Execute($"CREATE DATABASE {dbName}");
        }
    }
}
