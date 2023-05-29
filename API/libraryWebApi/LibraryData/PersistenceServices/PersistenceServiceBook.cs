using LibraryData.Repositories;
using LibraryModel.Models;

namespace LibraryData.PersistenceServices
{
    public class PersistenceServiceBook : IPersistenceServiceBook
    {
        private readonly IBookRepository _repository;

        public PersistenceServiceBook(IBookRepository repository)
            =>_repository = repository;

        public Book? Delete(string isbn) 
            => _repository.Delete(isbn);

        public IEnumerable<Book> GetAll(int size, int number) 
            => _repository.GetAll(size, number);

        public Book? GetByISBN(string isbn) 
            => _repository.GetByISBN(isbn);

        public (IEnumerable<Book>, IEnumerable<Book>) Insert(IEnumerable<Book> books) 
            => _repository.Insert(books);

        public Book Modify(Book oldBook, Book updatedBook) 
            => _repository.Modify(oldBook, updatedBook);
    }
}
