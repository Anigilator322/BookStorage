using BookStorage.Core.Models;

namespace BookStorage.Core.Repositories
{
    public interface IBookRepository
    {
        int Add(Book book);
        Book GetById(int id);
        IEnumerable<Book> Search(string query);
    }
}