using LibraryModel.Models;

namespace LibraryData.PersistenceServices
{
    public interface IPersistenceServiceBook
    {
        public Book? Delete(string isbn);

        public IEnumerable<Book> GetAll(int size, int number);

        public Book? GetByISBN(string isbn);

        public (IEnumerable<Book>, IEnumerable<Book>) Insert(IEnumerable<Book> books);

        public Book Modify(Book oldBook, Book updatedBook);
    }
}
