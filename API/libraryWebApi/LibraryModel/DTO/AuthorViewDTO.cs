namespace LibraryModel.DTO
{
    public record AuthorViewDTO(string Name = "", string Surname = "", DateOnly? DateOfBirth = default)
    {
    }
}
