using LibraryModel.Models;

namespace LibraryData.PersistenceServices
{
    public interface IPersistenceServiceAuthor
    {
        public Author? Delete(int id);

        public IEnumerable<Author> GetAll();

        public Author? GetById(int id);

        public Author Insert(Author entity);

        public Author Modify(Author entity);
    }
}
