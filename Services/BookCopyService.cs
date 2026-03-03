using BusinessObjects.Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _repo;
        public BookCopyService(IBookCopyRepository repo) => _repo = repo;

        public IEnumerable<BookCopy> GetBookCopies() => _repo.GetBookCopies();
        public BookCopy? GetBookCopyById(int id) => _repo.GetBookCopyById(id);

    }
}
