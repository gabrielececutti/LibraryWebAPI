namespace LibraryModel.DTO
{
    public record BookViewDTO (string Title = "", int Year = default)
    {
        public AuthorViewDTO? Author { get; set; }
    }
}
