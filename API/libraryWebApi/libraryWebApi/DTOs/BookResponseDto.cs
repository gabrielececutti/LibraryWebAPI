namespace LibraryWebApi.DTOs
{
    public record BookResponseDTO(string Title = "", int Year = default)
    {
        public AuthorDto Author { get; set; } = null!;
    }
}
