using FluentMigrator;

namespace Marketplace.Infrastructure.Migrations.Versions;


[Migration((long)VersionNumber.CreateUserTable, "Create User table")]
public class Version000001 : Migration
{
    public override void Down() { }

    public override void Up()
    {
        var table = Create.Table("Users");
        BaseVersion.InsertBaseTable(table);

        table
        .WithColumn("Email").AsString(100).NotNullable()
        .WithColumn("Cpf").AsString(15).NotNullable()
        .WithColumn("Name").AsString(100).NotNullable()
        .WithColumn("LastName").AsString(100).NotNullable()
        .WithColumn("Password").AsString(2000).NotNullable();
    }
}
