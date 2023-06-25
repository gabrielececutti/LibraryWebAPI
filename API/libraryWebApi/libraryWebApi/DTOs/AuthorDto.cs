namespace LibraryWebApi.DTOs
{
    public record AuthorDto(string Name = "", string Surname = "", DateTime BirthDate = default)
    {
    }
}
