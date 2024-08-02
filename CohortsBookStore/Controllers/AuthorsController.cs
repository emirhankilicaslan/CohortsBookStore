using CohortsBookStore.DTOs.AuthorDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Services.Abstract;
using CohortsBookStore.Validation.AuthorValidator;
using Microsoft.AspNetCore.Mvc;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CohortsAuthorStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : Controller
{
    private readonly IAuthorService _authorService;
    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }
    [HttpGet]
     public async Task<IActionResult> GetAuthors()
     {
         var authors = await _authorService.GetAllAuthors();
         return Ok(authors);
     }
    [HttpGet("{authorId}")]
    public async Task<IActionResult> GetAuthorById(int authorId)
    {
        var author = await _authorService.GetAuthorById(authorId);
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
    {
        await _authorService.CreateAuthor(createAuthorDto);
        return Ok("Author created successfully !");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAuthor(UpdateAuthorDto updateAuthorDto)
    {
        await _authorService.UpdateAuthor(updateAuthorDto);
        return Ok("Author updated successfully !");
    }

    [HttpDelete("{authorId}")]
    public async Task<IActionResult> DeleteAuthor(int authorId)
    {
        await _authorService.DeleteAuthor(authorId);
        return Ok("Author deleted successfully !");
    }
}