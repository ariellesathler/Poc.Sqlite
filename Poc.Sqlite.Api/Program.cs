using Dapper;
using LiteDB;
using Microsoft.Data.Sqlite;
using Poc.Sqlite.Api.BackgroundServices;
using Poc.Sqlite.Api.Domain;
using Poc.Sqlite.Api.Infra;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbConnection>(db => new SqliteConnection("Data Source=LocalDatabase.db"));
builder.Services.AddScoped<ILiteDatabase>(db => new LiteDatabase("Filename=LocalDatabaseNoSql.db"));
builder.Services.AddKeyedScoped<IUserRepository, UserRepository>("Sql");
builder.Services.AddKeyedScoped<IUserRepository, UserRepositoryNoSql>("NoSql");
builder.Services.AddHostedService<SyncDbFileService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/users", async ([FromKeyedServices("Sql")] IUserRepository userRepository) =>
{
	return await userRepository.GetAll();
})
.WithName("GetUsers")
.WithOpenApi();

app.MapGet("/users/{id}", async ([FromKeyedServices("Sql")] IUserRepository userRepository, int id) =>
{
	return await userRepository.GetById(id);
})
.WithName("GetUsersById")
.WithOpenApi();

app.MapPost("/users", async ([FromKeyedServices("Sql")] IUserRepository userRepository, User user) =>
{
	await userRepository.Insert(user);
})
.WithName("PostUsers")
.WithOpenApi();

app.Run();
