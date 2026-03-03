using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookCopyService
    {
        IEnumerable<BookCopy> GetBookCopies();
        BookCopy? GetBookCopyById(int id);
    }
}
