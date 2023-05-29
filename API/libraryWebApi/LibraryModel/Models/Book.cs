namespace LibraryModel.Models
{
    public record Book(int? Id = default, string Isbn = "", string Title = "", int Year = default) 
    {
        public Author? Author { get; set; }
        public int AuthorId { get; set; }
    }
}
