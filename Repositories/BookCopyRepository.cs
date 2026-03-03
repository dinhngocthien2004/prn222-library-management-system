using BusinessObjects.Entities;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly BookCopyDAO _dao;
        public BookCopyRepository(BookCopyDAO dao) => _dao = dao;

        public IEnumerable<BookCopy> GetBookCopies() => _dao.GetAll();
        public BookCopy? GetBookCopyById(int id) => _dao.GetById(id);
    }
}
