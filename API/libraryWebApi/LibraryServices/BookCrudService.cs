using LibraryData.Repositories;
using LibraryMapper;
using LibraryModel;
using LibraryModel.DTO;

namespace LibraryServices
{
    public class BookCrudService : IBookCrudService
    {
        private readonly IBookRepository _bookRepository;

        public BookCrudService(IBookRepository bookRepository)
            => _bookRepository = bookRepository;

        public BookViewDTO? Delete(string isbn)
            => _bookRepository.Delete(isbn)?.ToViewDTO();

        public IEnumerable<BookViewDTO> GetAll(int size, int number)
            => _bookRepository.GetAll(size, number).ToViewDTO();

        public BookViewDTO? GetByISBN(string isbn)
            => _bookRepository.GetByISBN(isbn)?.ToViewDTO();

        public BookInsertedResponseViewDTO Insert(IEnumerable<BookInsertDTO> books)
        {
            var response =  _bookRepository.Insert(books.ToEntity()).ToViewDTO();
            return new BookInsertedResponseViewDTO
            {
                InsertedBooks = response.Item1,
                RejectedBooks = response.Item2,
            };
        }

        public BookViewDTO  Update(BookInsertDTO book, string isbn) 
        {
            var existingBook = _bookRepository.GetByISBN(isbn) ?? throw new BookNotFoundException();
            var updateBook = _bookRepository.Modify(existingBook, book.ToEntity()!).ToViewDTO();
            return updateBook!;
        }
    }
}
