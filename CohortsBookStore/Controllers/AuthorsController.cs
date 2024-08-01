using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTOs.AuthorDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Validation.AuthorValidator;
using Microsoft.AspNetCore.Mvc;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CohortsAuthorStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : Controller
{
    private readonly BookStoreDbContext _bookStoreDbContext;
    private readonly IMapper _mapper;

    public AuthorsController(BookStoreDbContext bookStoreDbContext, IMapper mapper)
    {
        _bookStoreDbContext = bookStoreDbContext;
        _mapper = mapper;
    }
    [HttpGet]
     public async Task<IActionResult> GetAuthors()
     {
         var authors = _bookStoreDbContext.Authors.OrderBy(x => x.Id).ToList<Author>();
         return Ok(authors);
     }
    [HttpGet("GetAuthorById")]
    public async Task<IActionResult> GetAuthorById([FromQuery]ByIdAuthorDto byIdAuthorDto)
    {
        var author = _bookStoreDbContext.Authors.FindAsync(byIdAuthorDto.Id);
        
        ByIdAuthorValidator byIdAuthorValidator = new ByIdAuthorValidator();
        ValidationResult results = byIdAuthorValidator.Validate(byIdAuthorDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        return Ok(author.Result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
    {
        CreateAuthorValidator createAuthorValidator = new CreateAuthorValidator();
        ValidationResult results = createAuthorValidator.Validate(createAuthorDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }

        Author author = new Author();
        author = _mapper.Map<Author>(createAuthorDto);

        _bookStoreDbContext.Authors.Add(author);
        _bookStoreDbContext.SaveChanges();
        return Ok("Author created.");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAuthor(UpdateAuthorDto updateAuthorDto)
    {
        var author = _bookStoreDbContext.Authors.SingleOrDefault(x => x.Id == updateAuthorDto.Id);
        
        UpdateAuthorValidator updateAuthorValidator = new UpdateAuthorValidator();
        ValidationResult results = updateAuthorValidator.Validate(updateAuthorDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        if (author is null)
        {
            return BadRequest("Author does not exists.");
        }

        author.Name = updateAuthorDto.Name;
        author.Surname = updateAuthorDto.Surname;
        author.BirthDate = updateAuthorDto.BirthDate;

        _bookStoreDbContext.Authors.Update(author);
        _bookStoreDbContext.SaveChanges();
        return Ok("Author updated.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAuthor(ByIdAuthorDto byIdAuthorDto)
    {
        var author = _bookStoreDbContext.Authors.SingleOrDefault(x => x.Id == byIdAuthorDto.Id);
        
        ByIdAuthorValidator byIdAuthorValidator = new ByIdAuthorValidator();
        ValidationResult results = byIdAuthorValidator.Validate(byIdAuthorDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        if (author is null)
            return BadRequest("Author does not exists.");
        _bookStoreDbContext.Authors.Remove(author);
        _bookStoreDbContext.SaveChanges();
        return Ok("Author deleted.");
    }
}