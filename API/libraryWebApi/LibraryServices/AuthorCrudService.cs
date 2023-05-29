using LibraryData.PersistenceServices;
using LibraryMapper;
using LibraryModel.DTO;
using LibraryModel.Models;

namespace LibraryServices
{
    public class AuthorCrudService : IAuthorCrudService
    {
        private readonly IPersistenceServiceAuthor _persistenceServiceAuthor;

        public AuthorCrudService(IPersistenceServiceAuthor persistenceServiceAuthor)
           => _persistenceServiceAuthor = persistenceServiceAuthor;

        public AuthorViewDTO Create(AuthorDTO author)
            => _persistenceServiceAuthor.Insert(author.ToEntity()!).ToViewDTO()!;

        public AuthorViewDTO? Delete(int id)
            => _persistenceServiceAuthor.Delete(id)?.ToViewDTO();

        public IEnumerable<AuthorViewDTO> GetAll() 
            => _persistenceServiceAuthor.GetAll().ToViewDTO();

        public AuthorViewDTO? GetById(int id)
            => _persistenceServiceAuthor.GetById(id)?.ToViewDTO();

        public AuthorViewDTO Update(Author author) 
            => _persistenceServiceAuthor.Modify(author).ToViewDTO()!;
    }
}
