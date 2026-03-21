using BusinessObjects.Entities;
using DataAccessObjects;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly BookCopyDAO _dao;

        public BookCopyRepository(BookCopyDAO dao)
        {
            _dao = dao;
        }

        // ✅ FIX: trả IEnumerable đúng interface
        public IEnumerable<BookCopy> GetBookCopies()
        {
            return _dao.GetAll();
        }

        // ✅ nếu interface yêu cầu List thì giữ List
        public List<BookCopy> GetAll()
        {
            return _dao.GetAll().ToList();
        }

        public BookCopy GetBookCopyById(int id)
        {
            return _dao.GetById(id);
        }

        public void Update(BookCopy copy)
        {
            _dao.Update(copy);
        }
        public void AddBookCopy(BookCopy copy)
        {
            _dao.AddBookCopy(copy);
        }

    }
}