using CohortsBookStore.Context;
using CohortsBookStore.DTO_s.BookDtos;
using CohortsBookStore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CohortsBookStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : Controller
{
    private BookStoreDbContext _bookStoreDbContext;

    public BooksController(BookStoreDbContext bookStoreDbContext)
    {
        _bookStoreDbContext = bookStoreDbContext;
    }
    [HttpGet]
     public async Task<IActionResult> GetBooks()
     {
         var books = _bookStoreDbContext.Books.OrderBy(x => x.Id).ToList<Book>();
         return Ok(books);
     }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = _bookStoreDbContext.Books.Where(x => x.Id == id).SingleOrDefault();
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto createBookDto)
    {
        var book = _bookStoreDbContext.Books.SingleOrDefault(x => x.Title == createBookDto.Title);
        if (book is not null)
        {
            return BadRequest("Book already exists.");
        }

        book = new Book();
        book.Title = createBookDto.Title;
        book.GenreId = createBookDto.GenreId;
        book.PageCount = createBookDto.PageCount;
        book.PublishDate = createBookDto.PublishDate;

        _bookStoreDbContext.Books.Add(book);
        _bookStoreDbContext.SaveChanges();
        return Ok("Book created.");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateBook(UpdateBookDto updateBookDto)
    {
        var book = _bookStoreDbContext.Books.SingleOrDefault(x => x.Id == updateBookDto.Id);
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
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = _bookStoreDbContext.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest("Kitap bulunamadı.");
        _bookStoreDbContext.Books.Remove(book);
        _bookStoreDbContext.SaveChanges();
        return Ok("Book deleted.");
    }
}