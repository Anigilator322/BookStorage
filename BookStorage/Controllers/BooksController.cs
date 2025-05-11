using BookStorage.Core.Dtos;
using BookStorage.Core.Models;
using BookStorage.Core.Repositories;
using BookStorage.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string query)
        {
            var results = _bookService.Search(string.IsNullOrEmpty(query) ? "" : query);
            var dtos = results.Select(x => x.GetDto()).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null) return NotFound();
            var dto = book.GetDto();
            return Ok(dto);
        }

        [HttpGet("{id}/download")]
        public IActionResult Download(int id)
        {
            var book = _bookService.GetById(id);
            var filePath = Path.Combine("wwwroot", "books", book.FilePath);
            if (!System.IO.File.Exists(filePath)) return NotFound();
            return File(book.BookFileBytes, "application/octet-stream", Path.GetFileName(filePath));
        }

        [HttpPost]
        public IActionResult Add([FromBody] BookDto book)
        {
            var createdBook = _bookService.CreateBook(book);
            var dto = createdBook.GetDto();
            return Ok(dto);
        }
    }
}
