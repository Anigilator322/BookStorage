using BookStorage.Core.Dtos;
using LiteDB;

namespace BookStorage.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        
        private byte[] _bookFileBytes;
        [BsonIgnore]
        public byte[] BookFileBytes 
        {
            get
            {
                if(_bookFileBytes is null || _bookFileBytes.Length == 0)
                {
                    var filePath = Path.Combine("wwwroot", "books", FilePath);
                    if (!System.IO.File.Exists(filePath)) return Array.Empty<byte>();

                    _bookFileBytes = System.IO.File.ReadAllBytes(filePath);
                }
                return _bookFileBytes;
            } 
        }

        public BookDto GetDto()
        {
            return new BookDto()
            {
                Id = Id,
                Title = Title,
                Author = Author,
                Genre = Genre,
                Year = Year,
                Description = Description,
                FilePath = FilePath
            };
        }
    }
}
