using LibraryPersistenceLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryPersistenceLayer.Configurations;

public class AuthorConfigurtion : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasData
            (
                new Author { Id = 1, Name = "William", Surname = "Shakespeare", BirthDate = new DateTime(1564, 4, 26) },
                new Author { Id = 2, Name = "Jane ", Surname = "Austen", BirthDate = new DateTime(1775, 12, 16) },
                new Author { Id = 3, Name = "Ernest ", Surname = "Hemingway", BirthDate = new DateTime(1899, 7, 21) },
                new Author { Id = 4, Name = "Virginia  ", Surname = "Woolf", BirthDate = new DateTime(1882, 1, 25) }
            );
    }
}