using Marketplace.Application;
using Marketplace.Application.Services.AutoMapper;
using Marketplace.Infrastructure.Migrations;
using Marketplace.Domain.Extension;
using Marketplace.Infrastructure;
using Marketplace.Api.Filtros;

var builder = WebApplication.CreateBuilder(args);

// route lower case 

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// custom services

// auto mapper

builder.Services.AddScoped(prov => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfig());
}).CreateMapper());

// exceptions

builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));

// bootstrapper application

builder.Services.AddingInfrastructure(builder.Configuration);

builder.Services.AddApplication(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

UpdateDatabase();

app.Run();




void UpdateDatabase()
{
    var connectionString = builder.Configuration.GetConnection();
    var dbName = builder.Configuration.GetDatabaseName();

    Database.CreateDatabase(connectionString, dbName);

    app.MigrateDatabase();
}
