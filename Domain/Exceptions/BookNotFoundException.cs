using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions;

public class BookNotFoundException : NotFoundException
{
    public BookNotFoundException(Guid bookId) : base(
        $"The book requested with the identifier {bookId} was not found.")
    {
    }
}
