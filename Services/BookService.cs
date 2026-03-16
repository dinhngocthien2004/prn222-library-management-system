using BusinessObjects.Entities;
using Repositories;

namespace Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;

    public BookService(IBookRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _repo.GetBooks();
        }

        public Book? GetBookById(int id)
        {
            return _repo.GetBookById(id);
        }

        public void SaveBook(Book book)
        {
            _repo.SaveBook(book);
        }

        public void UpdateBook(Book book)
        {
            _repo.UpdateBook(book);
        }

        public void DeleteBook(Book book)
        {
            _repo.DeleteBook(book);
        }
    }

}
