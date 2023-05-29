using LibraryModel;
using LibraryModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Repositories
{
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
                    return author;
                }
                else throw new InvalidOperationException();
            }
            return author;
        }

        public IEnumerable<Author> GetAll()
        {
            return _dbContext.Authors
                .Where(author => author.Books.Any())
                .ToList();
        }

        public Author? GetById(int id)
        {
            return _dbContext.Authors
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == id);
        }

        public Author Insert(Author entity)
        {
            var author = _dbContext.Authors.Add(entity).Entity;
            _dbContext.SaveChanges();
            return author;
        }

        public Author Modify(Author entity)
        {
            var existingAuthor = _dbContext.Authors.FirstOrDefault(a => a.Id == entity.Id) ?? throw new AuthorNotFoundException();

            _dbContext.Entry(existingAuthor).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();

            return existingAuthor;
        }
    }
}
