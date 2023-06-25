using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPersistenceLayer.Models;

public record Author(int Id = default, string Name = null!, string Surname = null!, DateTime BirthDate = default)
{
    public virtual IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
}
