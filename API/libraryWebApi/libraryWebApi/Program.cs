using AutoMapper;
using LibraryBusinessLogic.AutoMappersProfiles;
using LibraryPersistenceLayer.Configurations;
using LibraryPersistenceLayer.Models;
using LibraryPersistenceLayer.Repositories.Abstract;
using LibraryPersistenceLayer.Repositories.Concrete;
using libraryWebApi;
using LibraryWebApi.DTOs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MapperConfigurationExpression configuration = new MapperConfigurationExpression();
configuration.AddProfiles(new List<Profile>() { new MainProfile() });

var mappingConfiguration = new MapperConfiguration(configuration);
mappingConfiguration.AssertConfigurationIsValid();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(MainProfile));
    cfg.AllowNullDestinationValues = true;
});


builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<Author, AuthorDto>()
              );

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
