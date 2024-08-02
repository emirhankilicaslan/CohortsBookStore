using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTO_s.BookDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Services.Abstract;
using CohortsBookStore.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CohortsBookStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : Controller
{
    private readonly IBookService _bookService;
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }
    [HttpGet]
     public async Task<IActionResult> GetBooks()
     {
         var books = await _bookService.GetAllBooks();
         return Ok(books);
     }
    [HttpGet("{bookId}")]
    public async Task<IActionResult> GetBookById(int bookId)
    {
        var book = await _bookService.GetBookById(bookId);
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto createBookDto)
    {
        await _bookService.CreateBook(createBookDto);
        return Ok("Book created successfully !");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateBook(UpdateBookDto updateBookDto)
    {
        await _bookService.UpdateBook(updateBookDto);
        return Ok("Book updated successfully !");
    }

    [HttpDelete("{bookId}")]
    public async Task<IActionResult> DeleteBook(int bookId)
    {
        await _bookService.DeleteBook(bookId);
        return Ok("Book deleted successfully !");
    }
}