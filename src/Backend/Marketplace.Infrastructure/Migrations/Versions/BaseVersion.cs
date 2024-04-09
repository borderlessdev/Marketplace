using FluentMigrator.Builders.Create.Table;

namespace Marketplace.Infrastructure.Migrations.Versions;

public static class BaseVersion
{
    public static ICreateTableColumnOptionOrWithColumnSyntax InsertBaseTable(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
    {
        return table
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreatedAt").AsDateTime().NotNullable();
    }
}
