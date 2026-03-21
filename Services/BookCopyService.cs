using BusinessObjects.Entities;
using Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _repo;

        public BookCopyService(IBookCopyRepository repo)
        {
            _repo = repo;
        }

        // ✅ FIX lỗi ở đây
        public List<BookCopy> GetAvailableCopies(int bookId)
        {
            return _repo.GetAll()
                .Where(c => c.BookId == bookId && c.IsAvailable)
                .ToList(); // 🔥 thêm dòng này
        }

        // ✅ thêm luôn để controller dùng
        public List<BookCopy> GetBookCopies()
        {
            return _repo.GetAll().ToList(); // 🔥 fix lỗi luôn
        }

        public void Update(BookCopy copy)
        {
            _repo.Update(copy);
        }

        public void AddBookCopy(BookCopy copy)
        {
            _repo.AddBookCopy(copy);
        }
    }
}