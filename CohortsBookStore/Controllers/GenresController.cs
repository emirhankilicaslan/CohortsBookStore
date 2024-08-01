using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTOs.GenreDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Validation.GenreValidator;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CohortsBookStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : Controller
{
    private readonly BookStoreDbContext _bookStoreDbContext;
    private readonly IMapper _mapper;

    public GenresController(BookStoreDbContext bookStoreDbContext, IMapper mapper)
    {
        _bookStoreDbContext = bookStoreDbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        var genres = _bookStoreDbContext.Genres.OrderBy(x => x.Id).ToList<Genre>();
        return Ok(genres);
    }

    [HttpGet("id")]
    public IActionResult GetGenresById([FromQuery]ByIdGenreDto byIdGenreDto)
    {
        var genre = _bookStoreDbContext.Genres.FindAsync(byIdGenreDto.Id);
        
        ByIdGenreValidator byIdGenreValidator = new ByIdGenreValidator();
        ValidationResult results = byIdGenreValidator.Validate(byIdGenreDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        
        return Ok(genre.Result);
    }

    [HttpPost]
    public IActionResult AddGenre(CreateGenreDto createGenreDto)
    {
        var genre = _bookStoreDbContext.Genres.SingleOrDefault(x => x.Name == createGenreDto.Name);
        
        
        CreateGenreValidator createGenreValidator = new CreateGenreValidator();
        ValidationResult results = createGenreValidator.Validate(createGenreDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        if (genre is not null)
        {
            return BadRequest("Genre already exists.");
        }
        genre = _mapper.Map<Genre>(createGenreDto);
        
        _bookStoreDbContext.Genres.Add(genre);
        _bookStoreDbContext.SaveChanges();
        return Ok("Genre created.");
    }

    [HttpPut]
    public IActionResult UpdateGenre(UpdateGenreDto updateGenreDto)
    {
        var genre = _bookStoreDbContext.Genres.SingleOrDefault(x => x.Id == updateGenreDto.Id);
        
        UpdateGenreValidator updateGenreValidator = new UpdateGenreValidator();
        ValidationResult results = updateGenreValidator.Validate(updateGenreDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        if (genre is null)
        {
            return BadRequest("Genre does not exists.");
        }

        genre.Name = updateGenreDto.Name;
        genre.IsActive = updateGenreDto.IsActive;

        _bookStoreDbContext.Genres.Update(genre);
        _bookStoreDbContext.SaveChanges();
        return Ok("Genre updated.");
    }

    [HttpDelete]
    public IActionResult DeleteGenre(ByIdGenreDto byIdGenreDto)
    {
        var genre = _bookStoreDbContext.Genres.SingleOrDefault(x => x.Id == byIdGenreDto.Id);
        
        ByIdGenreValidator byIdGenreValidator = new ByIdGenreValidator();
        ValidationResult results = byIdGenreValidator.Validate(byIdGenreDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        if (genre is null)
            return BadRequest("Genre does not exists.");
        
        _bookStoreDbContext.Genres.Remove(genre);
        _bookStoreDbContext.SaveChanges();
        return Ok("Genre deleted.");
    }

}