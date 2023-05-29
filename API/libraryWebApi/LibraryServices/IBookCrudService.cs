using LibraryModel.DTO;

namespace LibraryServices
{
    public interface IBookCrudService
    {
        public IEnumerable<BookViewDTO> GetAll(int size, int number);
        public BookViewDTO? GetByISBN(string isbn);
        public BookInsertedResponseViewDTO Insert(IEnumerable<BookInsertDTO> books);
        public BookViewDTO Update(BookInsertDTO book, string isbn);
        public BookViewDTO? Delete(string isbn);
    }
}
