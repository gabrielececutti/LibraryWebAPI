using LibraryPersistenceLayer.Configurations;
using LibraryPersistenceLayer.Exceptions;
using LibraryPersistenceLayer.Models;
using LibraryPersistenceLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryPersistenceLayer.Repositories.Concrete;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _dbContext;

    public BookRepository(LibraryDbContext dbContext)
        => _dbContext = dbContext;

    public Book? Delete(string isbn)
    {
        var book = _dbContext.Books.Include(b => b.Author).FirstOrDefault(p => p.ISBN == isbn);
        if (book is null) return null;
        _dbContext.Remove(book);
        _dbContext.SaveChanges();
        return book!;
    }

    public IEnumerable<Book> GetAll(int size, int number) 
    {
        int skipCount = size * number;
        return _dbContext.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .Skip(skipCount)
            .Take(size).AsEnumerable();
    }

    public Book? GetByISBN(string isbn)
        => _dbContext.Books.AsNoTracking().Include(b => b.Author).FirstOrDefault(p => p.ISBN == isbn);

    public (IEnumerable<Book>, IEnumerable<Book>) Insert(IEnumerable<Book> books)
    {
        var insertedBooks = new List<Book>();
        var rejectedBooks = new List<Book>();
        foreach (var book in books)
        {
            var existingBook = _dbContext.Books.FirstOrDefault(b => b.ISBN == book.ISBN);
            var existingAuthor = _dbContext.Authors.FirstOrDefault(a => a.Id == book.AuthorId);

            if (existingBook != null || existingAuthor == null)
            {
                rejectedBooks.Add(book);
                continue;
            }

            book.Author = existingAuthor;
            _dbContext.Books.Add(book);
            insertedBooks.Add(book);
        }
        _dbContext.SaveChanges();
        return (insertedBooks, rejectedBooks);
    }

    public Book Modify(Book book)
    {
        var current = _dbContext.Books.AsNoTracking().FirstOrDefault(b => b.Id == book.Id);
        var existingAuthor = _dbContext.Authors.Find(book.AuthorId) ?? throw new AuthorNotFoundException();

        var entity = _dbContext.Books.Update(current! with { Title = book.Title, ISBN = book.ISBN, Year = book.Year, AuthorId = book.AuthorId });
        _dbContext.SaveChanges();

        return entity.Entity;
    }
}