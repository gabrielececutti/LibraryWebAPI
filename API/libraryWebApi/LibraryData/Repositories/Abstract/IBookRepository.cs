using LibraryPersistenceLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPersistenceLayer.Repositories.Abstract;

public interface IBookRepository
{
    public Book? Delete(string isbn);

    public IEnumerable<Book> GetAll(int size, int number);

    public Book? GetByISBN(string isbn);

    public (IEnumerable<Book>, IEnumerable<Book>) Insert(IEnumerable<Book> books);

    public Book Modify(Book book);
}
