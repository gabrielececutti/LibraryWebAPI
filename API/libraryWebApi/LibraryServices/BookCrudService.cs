using LibraryData.PersistenceServices;
using LibraryMapper;
using LibraryModel;
using LibraryModel.DTO;

namespace LibraryServices
{
    public class BookCrudService : IBookCrudService
    {
        private readonly IPersistenceServiceBook _persistenceServiceBook;

        public BookCrudService(IPersistenceServiceBook persistenceServiceBook)
            => _persistenceServiceBook = persistenceServiceBook;

        public BookViewDTO? Delete(string isbn)
            => _persistenceServiceBook.Delete(isbn)?.ToViewDTO();

        public IEnumerable<BookViewDTO> GetAll(int size, int number)
            => _persistenceServiceBook.GetAll(size, number).ToViewDTO();

        public BookViewDTO? GetByISBN(string isbn)
            => _persistenceServiceBook.GetByISBN(isbn)?.ToViewDTO();

        public BookInsertedResponseViewDTO Insert(IEnumerable<BookInsertDTO> books)
        {
            var response =  _persistenceServiceBook.Insert(books.ToEntity()).ToViewDTO();
            return new BookInsertedResponseViewDTO
            {
                InsertedBooks = response.Item1,
                RejectedBooks = response.Item2,
            };
        }

        public BookViewDTO  Update(BookInsertDTO book, string isbn) 
        {
            var existingBook = _persistenceServiceBook.GetByISBN(isbn) ?? throw new BookNotFoundException();
            var updateBook = _persistenceServiceBook.Modify(existingBook, book.ToEntity()!).ToViewDTO();
            return updateBook!;
        }
    }
}
