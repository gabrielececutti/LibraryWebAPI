using AutoMapper;
using LibraryModel;
using LibraryPersistenceLayer.Exceptions;
using LibraryPersistenceLayer.Models;
using LibraryPersistenceLayer.Repositories.Abstract;
using LibraryWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace libraryWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, ILogger<AuthorController> logger, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult? GetAll(int size, int number)
        {
            if (size < 0 || number < 0) return BadRequest("Inserisci dei numeri validi!");
            try
            {
                var books = _mapper.Map<IEnumerable<BookResponseDTO>>(_bookRepository.GetAll(size, number));
                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }

        [HttpGet]
        public IActionResult? GetByISBN(string isbn)
        {
            if (!IsbnValidator.IsValid(isbn)) return BadRequest("ISBN non valido!");
            try
            {
                var book = _mapper.Map<BookResponseDTO>(_bookRepository.GetByISBN(isbn));
                return (book != default)
                    ? Ok(book)
                    : NotFound("Libro non trovato!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }

        [HttpPost]
        public IActionResult? Insert([FromBody] IEnumerable<BookInsertDTO> books)
        {
            if (!books.All(b => IsbnValidator.IsValid(b.Isbn))) return BadRequest("Uno o più ISBN non validi!");
            try
            {
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Non puoi inserire libri con un ISBN già esistente!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }

        [HttpPut]
        public IActionResult? Update([FromRoute] string isbn, [FromBody] BookInsertDTO book)
        {
            if (!IsbnValidator.IsValid(isbn)) return BadRequest("ISBN non valido!");
            if (!IsbnValidator.IsValid(book.Isbn)) return BadRequest("nuovo ISBN non valido");
            try
            {
                var toUpdate = _mapper.Map<Book>(book);
                var bookUpdated = _mapper.Map<BookResponseDTO>(_bookRepository.Modify(toUpdate));
                return Ok(bookUpdated);
            }
            catch (BookNotFoundException)
            {
                return NotFound("Non eiste un libro con quell'ISBN!");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Quell'ISBN esiste già!");
            }
            catch (AuthorNotFoundException)
            {
                return NotFound("Non esiste un autore con quell'Id!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }

        [HttpDelete]
        public IActionResult? Delete(string isbn)
        {
            if (!IsbnValidator.IsValid(isbn)) return BadRequest("ISBN non valido!");
            try
            {
                var book = _mapper.Map<BookResponseDTO>(_bookRepository.Delete(isbn));
                return book != default
                    ? Ok(book) :
                    NotFound("Non esiste un libro associato con quell'ISBN!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }
    }
}