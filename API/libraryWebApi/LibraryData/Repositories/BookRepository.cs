using LibraryModel;
using LibraryModel.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryData.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
            => _dbContext = dbContext;

        public Book? Delete(string isbn)
        {
            var book = _dbContext.Books.Include(b => b.Author).FirstOrDefault(p => p.Isbn.Equals(isbn));
            if (book is null) return null;
            _dbContext.Remove(book);
            _dbContext.SaveChanges();
            return book;
        }

        public IEnumerable<Book> GetAll(int size, int number)
        {
            int skipCount = size * number;
            var books = _dbContext.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Skip(skipCount)
                .Take(size)
                .ToList();
            return books;
        }

        public Book? GetByISBN(string isbn)
        {
            return _dbContext.Books.AsNoTracking().Include( b => b.Author).FirstOrDefault(p => p.Isbn.Equals(isbn));
        }

        public (IEnumerable<Book>, IEnumerable<Book>) Insert(IEnumerable<Book> books) 
        {
            var insertedBooks = new List<Book>();
            var rejectedBooks = new List<Book>();
            foreach (var book in books)
            {
                var existingBook = _dbContext.Books.FirstOrDefault(b => b.Isbn == book.Isbn);
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

        public Book Modify(Book oldBook, Book updatedBook)
        {
            if (updatedBook.Isbn != oldBook.Isbn && _dbContext.Books.Any(b => b.Isbn == updatedBook.Isbn))
            {
                throw new InvalidOperationException();
            }
            var existingAuthor = _dbContext.Authors.Find(updatedBook.AuthorId) ?? throw new AuthorNotFoundException();

            _dbContext.Entry(oldBook).State = EntityState.Deleted;
            _dbContext.Entry(updatedBook).State = EntityState.Added;
            _dbContext.SaveChanges();

            return updatedBook;
        }
    }
}
