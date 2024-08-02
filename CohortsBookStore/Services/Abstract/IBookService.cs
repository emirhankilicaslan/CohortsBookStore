using CohortsBookStore.DTO_s.BookDtos;

namespace CohortsBookStore.Services.Abstract;

public interface IBookService
{
    Task<List<ResultBookDto>> GetAllBooks();
    Task CreateBook(CreateBookDto createBookDto);
    Task UpdateBook(UpdateBookDto updateBookDto);
    Task DeleteBook(int bookId);
    Task<GetBookByIdDto> GetBookById(int bookId);
}