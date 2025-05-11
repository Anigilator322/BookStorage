using BookStorage.Core.Models;
using LiteDB;

namespace BookStorage.Core.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LiteDatabase _db;
        private readonly ILiteCollection<Book> _books;

        public BookRepository()
        {
            _db = new LiteDatabase("Filename=Library.db;Connection=shared");
            _books = _db.GetCollection<Book>("books");
            _books.EnsureIndex(x => x.Id, true);
        }

        public IEnumerable<Book> Search(string query) =>
            _books.Find(x => x.Title.Contains(query) || x.Author.Contains(query));

        public Book GetById(int id) => _books.FindById(id);

        public int Add(Book book) => _books.Insert(book);
    }
}
