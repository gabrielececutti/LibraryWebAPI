using LibraryModel.Models;

namespace LibraryData.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll(int size, int number );
        Book? GetByISBN(string isbn);
        (IEnumerable<Book>, IEnumerable<Book>) Insert(IEnumerable<Book> books);
        Book Modify(Book oldBook, Book updatedBook);
        Book? Delete(string isbn);
    }

    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Author? GetById(int id);
        Author Insert(Author entity);
        Author Modify(Author entity);
        Author? Delete(int id);
    }
}
