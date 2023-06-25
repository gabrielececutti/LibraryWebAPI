using LibraryPersistenceLayer.Configurations;
using LibraryPersistenceLayer.Exceptions;
using LibraryPersistenceLayer.Models;
using LibraryPersistenceLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryPersistenceLayer.Repositories.Concrete;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _dbContext;

    public AuthorRepository(LibraryDbContext dbContext)
        => _dbContext = dbContext;

    public Author? Delete(int id)
    {
        var author = _dbContext.Authors
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

        if (author != null)
        {
            bool hasAssociatedBooks = _dbContext.Books.Any(b => b.AuthorId == id);
            if (!hasAssociatedBooks)
            {
                _dbContext.Authors.Remove(author);
                _dbContext.SaveChanges();
                return author!;
            }
            else throw new InvalidOperationException();
        }
        return null;
    }

    public IEnumerable<Author> GetAll()
    {
        return _dbContext.Authors
            .Where(author => author.Books.Any());
    }

    public Author? GetById(int id)
    {
        return _dbContext.Authors
            .AsNoTracking()
            .FirstOrDefault(a => a.Id == id);
    }

    public Author Insert(Author author)
    {
        var entity = _dbContext.Add(author);
        _dbContext.SaveChanges();
        return entity.Entity;
    }

    public Author Modify(Author author)
    {
        var current = _dbContext.Authors.AsNoTracking().FirstOrDefault(a => a.Id == author.Id) ?? throw new AuthorNotFoundException();
        var updated = _dbContext.Authors.Update(current with { Name = author.Name, Surname = author.Surname, BirthDate = author.BirthDate });
        _dbContext.SaveChanges();
        return updated.Entity;
    }
}
