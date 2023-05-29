using LibraryData.Repositories;
using LibraryModel.Models;

namespace LibraryData.PersistenceServices
{
    public class PersistenceServiceAuthor : IPersistenceServiceAuthor
    { 
        private readonly IAuthorRepository _repository;

        public PersistenceServiceAuthor(IAuthorRepository repository)
            => _repository = repository;

        public Author? Delete(int id)
            => _repository.Delete(id);

        public IEnumerable<Author> GetAll() 
            => _repository.GetAll();

        public Author? GetById(int id) 
            => _repository.GetById(id);

        public Author Insert(Author entity) 
            => _repository.Insert(entity);

        public Author Modify(Author entity) 
            => _repository.Modify(entity);
    }
}
