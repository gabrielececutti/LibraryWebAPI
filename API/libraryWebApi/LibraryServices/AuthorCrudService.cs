using LibraryData.Repositories;
using LibraryMapper;
using LibraryModel.DTO;
using LibraryModel.Models;

namespace LibraryServices
{
    public class AuthorCrudService : IAuthorCrudService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorCrudService(IAuthorRepository authorRepository)
           => _authorRepository = authorRepository;

        public AuthorViewDTO Create(AuthorDTO author)
            => _authorRepository.Insert(author.ToEntity()!).ToViewDTO()!;

        public AuthorViewDTO? Delete(int id)
            => _authorRepository.Delete(id)?.ToViewDTO();

        public IEnumerable<AuthorViewDTO> GetAll() 
            => _authorRepository.GetAll().ToViewDTO();

        public AuthorViewDTO? GetById(int id)
            => _authorRepository.GetById(id)?.ToViewDTO();

        public AuthorViewDTO Update(Author author) 
            => _authorRepository.Modify(author).ToViewDTO()!;
    }
}
