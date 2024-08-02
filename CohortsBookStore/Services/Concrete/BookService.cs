using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTO_s.BookDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Exceptions;
using CohortsBookStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CohortsBookStore.Services.Concrete;

public class BookService : IBookService
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    
    public BookService(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<ResultBookDto>> GetAllBooks()
    {
        var books = await _context.Books.ToListAsync();
        return _mapper.Map<List<ResultBookDto>>(books);
    }

    public async Task CreateBook(CreateBookDto createBookDto)
    {
        var book = _mapper.Map<Book>(createBookDto);
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBook(UpdateBookDto updateBookDto)
    {
        var bookExist = await _context.Books.FindAsync(updateBookDto.Id);

        if (bookExist == null)
            throw new NotFoundException($"Book with ID {updateBookDto.Id} not found.");

        _mapper.Map(updateBookDto, bookExist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBook(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);

        if (book == null)
            throw new NotFoundException($"Book with ID {bookId} not found.");

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<GetBookByIdDto> GetBookById(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);

        if (book == null)
            throw new NotFoundException($"Book with ID {bookId} not found.");

        return _mapper.Map<GetBookByIdDto>(book);
    }
}