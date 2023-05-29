namespace LibraryModel.DTO
{
    public class BookInsertedResponseViewDTO
    {
        public IEnumerable<BookViewDTO> InsertedBooks { get; set; } = new List<BookViewDTO>();
        public IEnumerable<BookViewDTO> RejectedBooks { get; set; } = new List<BookViewDTO>();
    }
}
