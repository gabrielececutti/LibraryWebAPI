namespace LibraryWebApi.DTOs
{
    public record BookInsertDTO(string Title = "", int Year = default, string Isbn = "", int AuthorId = default)
    {
    }
}
