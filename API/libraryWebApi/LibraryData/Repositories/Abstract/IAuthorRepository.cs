using LibraryPersistenceLayer.Models;

namespace LibraryPersistenceLayer.Repositories.Abstract;

public interface IAuthorRepository
{
    public Author? Delete(int id);

    public IEnumerable<Author> GetAll();

    public Author? GetById(int id);

    public Author Insert(Author author);

    public Author Modify(Author author);
}
