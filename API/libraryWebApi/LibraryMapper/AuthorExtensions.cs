using LibraryModel.DTO;
using LibraryModel.Models;

namespace LibraryMapper
{
    public static class AuthorExtensions
    {
        public static AuthorDTO? ToDTO(this Author author)
        {
            if (author == null) return null;
            return new AuthorDTO
            {
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth
            };   
        }

        public static AuthorViewDTO? ToViewDTO(this Author author)
        {
            if (author == null) return null;
            return new AuthorViewDTO
            {
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = DateOnly.FromDateTime(author.DateOfBirth ?? DateTime.MinValue)
            };
        }

        public static Author? ToEntity (this AuthorDTO authorDTO)
        {
            if (authorDTO == null) return null; 
            return new Author
            {
                Name = authorDTO.Name,
                Surname = authorDTO.Surname,
                DateOfBirth = authorDTO.DateOfBirth
            };
        }

        public static IEnumerable<AuthorViewDTO> ToViewDTO (this IEnumerable<Author> authors)
        { 
            return authors.Select(a => new AuthorViewDTO
            {
                Name = a.Name,
                Surname = a.Surname,
                DateOfBirth = DateOnly.FromDateTime(a.DateOfBirth ?? DateTime.MinValue)
            });
        }
    }
}
