using LibraryPersistenceLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPersistenceLayer.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasIndex(b => b.ISBN)
                .IsUnique();
            builder.HasData
                (
                    new Book { Id = 1, Title = "Macbeth", ISBN = "9780143132188", Year = "1606", AuthorId = 1 },
                    new Book { Id = 2, Title = "Romeo e Giulietta", ISBN = "9780743477116", Year = "1597", AuthorId = 1 },
                    new Book { Id = 3, Title = "Orgoglio e pregiudizio", ISBN = "9780141439518", Year = "1813", AuthorId = 2 },
                    new Book { Id = 4, Title = "Ragione e sentimento", ISBN = "9780141439662", Year = "1811", AuthorId = 2 },
                    new Book { Id = 5, Title = "Il vecchio e il mare", ISBN = "9780684801223", Year = "1952", AuthorId = 3 },
                    new Book { Id = 6, Title = "Addio alle armi", ISBN = "9780099910107", Year = "1929", AuthorId = 3 }
                );
        }
    }
}
