namespace LibraryModel.DTO
{
    public record AuthorDTO (string Name = "" ,string Surname = "", DateTime? DateOfBirth = default)
    {
    }
}
