namespace LibraryPersistenceLayer.Models;

public record Book(int Id = default, string Title = null!, string ISBN = null!, string Year = null!)
{
    public int AuthorId { get; set; } = 0!;
    public virtual Author Author { get; set; } = null!;
}
