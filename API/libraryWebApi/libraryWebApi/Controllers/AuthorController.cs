using LibraryModel;
using LibraryModel.DTO;
using LibraryModel.Models;
using LibraryServices;
using Microsoft.AspNetCore.Mvc;

namespace libraryWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorCrudService _authorCrudService;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger, IAuthorCrudService authorCrudService)
        {
            _logger = logger;
            _authorCrudService = authorCrudService;
        }

        [HttpGet] 
        public IActionResult GetAll()
        {
            var authors = _authorCrudService.GetAll();
            return Ok(authors);
        }

        [HttpGet] 
        public IActionResult GetById(int id)
        {
            if (id == 0) return BadRequest("Id non valido!");
            try
            {
                var author = _authorCrudService.GetById(id);
                return (author == default)
                    ? NotFound("Autore non trovato!")
                    : Ok(author);
            }
            catch (Exception) {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }

        [HttpPost] 
        public IActionResult Create([FromBody] AuthorDTO author)
        {
            try
            {
                _authorCrudService.Create(author);
                return Ok(author);
            }catch (Exception)
            {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody]Author author)
        {
            if (author.Id == 0) return BadRequest("Id non valido!");
            try
            {
                return Ok(_authorCrudService.Update(author));
            }
            catch (AuthorNotFoundException)
            {
                return BadRequest("Non puoi modifcare un autore con un id non esistente!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }          
        }

        [HttpDelete] 
        public IActionResult Delete (int id)
        {
            if (id == 0) return BadRequest("id non valido!");
            try
            {
                var authorDeleted = _authorCrudService.Delete(id);
                return authorDeleted != default
                    ? Ok(authorDeleted)
                    : NotFound("Autore non trovato");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Non puoi eliminare un autore che ha associato dei libri!");
            }
            catch
            {
                return StatusCode(500, "Qualcosa è andato storto :(");
            }
        }
    }
}