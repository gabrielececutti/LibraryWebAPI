namespace LibraryModel.Models
{
    public record Author(int? Id = default, string Name = "", string Surname = "", DateTime? DateOfBirth = default) 
    {
        public IEnumerable<Book> Books = new HashSet<Book>();
    }
}
