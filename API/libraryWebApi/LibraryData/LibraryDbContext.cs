using LibraryModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Authors");
                entity.HasKey(a => a.Id); 
                entity.Property(a => a.Id).ValueGeneratedOnAdd(); 
                entity.Property(a => a.Name).IsRequired().HasMaxLength(50); 
                entity.Property(a => a.Surname).IsRequired().HasMaxLength(50);
                entity.Property(a => a.DateOfBirth).HasColumnType("date"); 
                entity.HasMany(a => a.Books) 
                    .WithOne(b => b.Author)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");
                entity.HasKey(b => b.Id); 
                entity.Property(b => b.Id).ValueGeneratedOnAdd(); 
                entity.Property(b => b.Isbn).IsRequired().HasMaxLength(20); 
                entity.Property(b => b.Title).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Year).IsRequired(); 
                entity.HasOne(b => b.Author) 
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId);
            });
        }

    }
}
