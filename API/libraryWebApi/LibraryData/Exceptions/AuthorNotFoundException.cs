using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPersistenceLayer.Exceptions;

public class AuthorNotFoundException : Exception
{
    public AuthorNotFoundException() : base() { }
}
