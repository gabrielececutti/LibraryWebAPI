using LibraryModel.DTO;
using LibraryModel.Models;

namespace LibraryServices
{
    public interface IAuthorCrudService
    {
        public IEnumerable<AuthorViewDTO> GetAll();
        public AuthorViewDTO? GetById(int id);
        public AuthorViewDTO Create(AuthorDTO author);
        public AuthorViewDTO Update(Author author);
        public AuthorViewDTO? Delete(int id);
    }
}
