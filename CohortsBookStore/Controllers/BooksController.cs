using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTO_s.BookDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CohortsBookStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : Controller
{
    private readonly BookStoreDbContext _bookStoreDbContext;
    private readonly IMapper _mapper;

    public BooksController(BookStoreDbContext bookStoreDbContext, IMapper mapper)
    {
        _bookStoreDbContext = bookStoreDbContext;
        _mapper = mapper;
    }
    [HttpGet]
     public async Task<IActionResult> GetBooks()
     {
         var books = _bookStoreDbContext.Books.OrderBy(x => x.Id).ToList<Book>();
         return Ok(books);
     }
    [HttpGet("GetBookById")]
    public async Task<IActionResult> GetBookById([FromQuery]ByIdBookDto byIdBookDto)
    {
        var book = _bookStoreDbContext.Books.FindAsync(byIdBookDto.Id);
        
        ByIdBookValidator byIdBookValidator = new ByIdBookValidator();
        ValidationResult results = byIdBookValidator.Validate(byIdBookDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        
        return Ok(book.Result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto createBookDto)
    {
        var book = _bookStoreDbContext.Books.SingleOrDefault(x => x.Title == createBookDto.Title);
        
        CreateBookValidator createBookValidator = new CreateBookValidator();
        ValidationResult results = createBookValidator.Validate(createBookDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        if (book is not null)
        {
            return BadRequest("Book already exists.");
        }
        book = _mapper.Map<Book>(createBookDto);

        _bookStoreDbContext.Books.Add(book);
        _bookStoreDbContext.SaveChanges();
        return Ok("Book created.");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateBook(UpdateBookDto updateBookDto)
    {
        var book = _bookStoreDbContext.Books.SingleOrDefault(x => x.Id == updateBookDto.Id);
        
        UpdateBookValidator updateBookValidator = new UpdateBookValidator();
        ValidationResult results = updateBookValidator.Validate(updateBookDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        if (book is null)
        {
            return BadRequest("Book does not exists.");
        }
        
        book.Title = updateBookDto.Title;
        book.GenreId = updateBookDto.GenreId;
        book.PageCount = updateBookDto.PageCount;
        book.PublishDate = updateBookDto.PublishDate;

        _bookStoreDbContext.Books.Update(book);
        _bookStoreDbContext.SaveChanges();
        return Ok("Book updated.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook(ByIdBookDto byIdBookDto)
    {
        var book = _bookStoreDbContext.Books.SingleOrDefault(x => x.Id == byIdBookDto.Id);
        
        ByIdBookValidator byIdBookValidator = new ByIdBookValidator();
        ValidationResult results = byIdBookValidator.Validate(byIdBookDto);
        if (!results.IsValid)
        {
            List<string> errorMessages = new List<string>();
            foreach (var failure in results.Errors)
            {
                errorMessages.Add(failure.ErrorMessage);
            }
            return BadRequest(errorMessages);
        }
        if (book is null)
            return BadRequest("Book does not exists.");
        _bookStoreDbContext.Books.Remove(book);
        _bookStoreDbContext.SaveChanges();
        return Ok("Book deleted.");
    }
}