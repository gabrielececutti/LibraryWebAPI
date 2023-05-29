using LibraryModel;
using LibraryModel.DTO;
using LibraryServices;
using Microsoft.AspNetCore.Mvc;

namespace libraryWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookCrudService _bookCrudService;
        private readonly ILogger<AuthorController> _logger;

        public BookController(ILogger<AuthorController> logger, IBookCrudService bookCrudService)
        {
            _logger = logger;
            _bookCrudService = bookCrudService;
        }

        [HttpGet]
        public IActionResult? GetAll(int size, int number)
        {
            if (size < 0 || number < 0) return BadRequest("Inserisci dei numeri validi!");
            try
            {
                return Ok(_bookCrudService.GetAll(size, number)); 
            } catch (Exception)
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
                var book = _bookCrudService.GetByISBN(isbn);
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
                return Ok(_bookCrudService.Insert(books));
            }
            catch (InvalidOperationException) {
                return BadRequest("Non puoi inserire libri con un ISBN già esistente!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }

        [HttpPut]
        public IActionResult? Update([FromBody] BookInsertDTO book, string isbn)
        {
            if (!IsbnValidator.IsValid(isbn)) return BadRequest("ISBN non valido!");
            if (!IsbnValidator.IsValid(book.Isbn)) return BadRequest("nuovo ISBN non valido");
            try
            {
                var bookUpdated = _bookCrudService.Update(book, isbn);
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
                var book = _bookCrudService.Delete(isbn);
                return book != default
                    ? Ok(book) :
                    NotFound("Non esiste un libro associato con quell'ISBN!");
            } catch(Exception) {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }
    }
}
