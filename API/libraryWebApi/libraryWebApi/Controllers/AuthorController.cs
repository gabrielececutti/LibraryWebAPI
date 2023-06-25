using AutoMapper;
using LibraryPersistenceLayer.Exceptions;
using LibraryPersistenceLayer.Models;
using LibraryPersistenceLayer.Repositories.Abstract;
using LibraryWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace libraryWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ILogger<AuthorController> _logger;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorRepository authorRepository, ILogger<AuthorController> logger, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var authors = _mapper.Map<IEnumerable<AuthorDto>>(_authorRepository.GetAll());
            return Ok(authors);
        }
        catch (Exception)
        {
            return StatusCode(500, "Qualcosa è andato storto :(");
        }

    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        if (id == 0) return BadRequest("Id non valido!");
        try
        {
            var x = _authorRepository.GetById(id);
            var author = _mapper.Map<AuthorDto>(_authorRepository.GetById(id)); 
            return (author == default)
                ? NotFound("Autore non trovato!")
                : Ok(author);
        }
        catch (Exception)
        {
            return StatusCode(500, "Qualcosa è andato storto :(");
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] AuthorDto author)
    {
        try
        {
            var entity = _mapper.Map<Author>(author);
            var result = _mapper.Map<AuthorDto>(_authorRepository.Insert(entity));
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Qualcosa è andato storto :(");
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] Author author)
    {
        if (author.Id == 0) return BadRequest("Id non valido!");
        try
        {
            var result = _mapper.Map<AuthorDto>(_authorRepository.Modify(author));
            return Ok(result);
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
    public IActionResult Delete(int id)
    {
        if (id == 0) return BadRequest("id non valido!");
        try
        {
            var authorDeleted = _mapper.Map<AuthorDto>(_authorRepository.Delete(id));
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