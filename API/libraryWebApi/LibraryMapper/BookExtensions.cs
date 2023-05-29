using LibraryModel.DTO;
using LibraryModel.Models;

namespace LibraryMapper
{
    public static class BookExtensions
    {
        public static BookViewDTO? ToViewDTO (this Book book)
        {
            if (book is null) return null;
            return new BookViewDTO
            {
                Title = book.Title,
                Year = book.Year,
                Author = book.Author?.ToViewDTO()
            };
        }

        public static Book? ToEntity (this BookInsertDTO bookInsertDTO)
        {
            if (bookInsertDTO is null) return null;
            return new Book
            {
                Title = bookInsertDTO.Title,
                Year = bookInsertDTO.Year,
                Isbn = bookInsertDTO.Isbn,
                AuthorId = bookInsertDTO.AuthorId,
            };
        }

        public static (IEnumerable<BookViewDTO>, IEnumerable<BookViewDTO>) ToViewDTO (this (IEnumerable<Book>, IEnumerable<Book>) books)
        {
            var validBooks = books.Item1.Select(b => new BookViewDTO
            {
                Title = b.Title,
                Year = b.Year,
                Author = b.Author?.ToViewDTO() 
            }); 

            var rejectedBooks = books.Item2.Select(b => new BookViewDTO
            {
                Title = b.Title,
                Year = b.Year,
                Author = b.Author?.ToViewDTO() 
            });

            return (validBooks, rejectedBooks);
        }

        public static IEnumerable<Book> ToEntity(this IEnumerable<BookInsertDTO> booksDTO)
        {
            return booksDTO.Select(bookDTO => new Book
            {
                Title = bookDTO.Title,
                Year = bookDTO.Year,
                Isbn = bookDTO.Isbn,
                AuthorId = bookDTO.AuthorId,
            });
        }

        public static IEnumerable<BookViewDTO> ToViewDTO(this IEnumerable<Book> books)
        {
            return books.Select(book => new BookViewDTO
            {
                Title = book.Title,
                Year = book.Year,
                Author = book.Author?.ToViewDTO() 
            });
        }
    }
}
