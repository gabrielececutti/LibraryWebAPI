using LibraryData;
using LibraryData.PersistenceServices;
using LibraryData.Repositories;
using LibraryModel;
using LibraryServices;
using libraryWebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPersistenceServiceBook, PersistenceServiceBook>();
builder.Services.AddScoped<IPersistenceServiceAuthor, PersistenceServiceAuthor>();
builder.Services.AddScoped<IAuthorCrudService, AuthorCrudService>();
builder.Services.AddScoped<IBookCrudService, BookCrudService>();

var app = builder.Build();

await app.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
