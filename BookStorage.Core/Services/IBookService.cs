using BookStorage.Core.Dtos;
using BookStorage.Core.Models;

namespace BookStorage.Core.Services
{
    public interface IBookService
    {
        Book CreateBook(BookDto book);
        Book GetById(int id);

        List<Book> Search(string query);
    }
}