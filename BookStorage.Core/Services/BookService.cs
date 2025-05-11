using BookStorage.Core.Dtos;
using BookStorage.Core.Models;
using BookStorage.Core.Repositories;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace BookStorage.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        public BookService(IBookRepository bookRepository)
        {
            _repository = bookRepository;
        }
        public Book GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Book CreateBook(BookDto bookDto)
        {
            var book = new Book
            {
                Id = 0,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Description = bookDto.Description,
                FilePath = bookDto.FilePath,
                Genre = bookDto.Genre,
                Year = bookDto.Year
            };
            var id = _repository.Add(book);
            return _repository.GetById(id);
        }

        public List<Book> Search(string query)
        {
            return _repository.Search(query).ToList();
        }
    }
}
