namespace LibraryWebApi.DTOs
{
    public class BooksInsertedResponseDto
    {
        public IEnumerable<BookResponseDTO> InsertedBooks { get; set; } = new List<BookResponseDTO>();
        public IEnumerable<BookResponseDTO> RejectedBooks { get; set; } = new List<BookResponseDTO>();
    }
}  