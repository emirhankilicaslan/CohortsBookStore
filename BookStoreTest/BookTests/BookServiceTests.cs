using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTO_s.BookDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Exceptions;
using CohortsBookStore.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookStoreTests.BookTests
{
    public class BookServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;

        public BookServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
        }

        private BookStoreDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new BookStoreDbContext(options);
            return context;
        }

        [Fact]
        public async Task CreateBook_Should_Add_Book()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new BookService(context, _mockMapper.Object);
            var createBookDto = new CreateBookDto { Title = "Test Book", PageCount = 100, PublishDate = new DateTime(1990, 1, 1), AuthorId = 1, GenreId = 1 };
            var book = new Book { Title = createBookDto.Title, PageCount = createBookDto.PageCount, PublishDate = createBookDto.PublishDate, AuthorId = createBookDto.AuthorId, GenreId = createBookDto.GenreId };

            _mockMapper.Setup(m => m.Map<Book>(It.IsAny<CreateBookDto>())).Returns(book);

            // Act
            await service.CreateBook(createBookDto);

            // Assert
            var createdBook = await context.Books.FirstOrDefaultAsync(b => b.Title == "Test Book");
            Assert.NotNull(createdBook);
            Assert.Equal(100, createdBook.PageCount);
            Assert.Equal(new DateTime(1990, 1, 1),createdBook.PublishDate);
            Assert.Equal(1, createdBook.AuthorId);
            Assert.Equal(1, createdBook.GenreId);
        }

        [Fact]
        public async Task DeleteBook_Should_Remove_Book_When_Book_Exists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new BookService(context, _mockMapper.Object);
            var book = new Book { Title = "Test Book", PageCount = 100, PublishDate = new DateTime(1990, 1, 1), GenreId = 1 };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            // Act
            await service.DeleteBook(book.Id);

            // Assert
            var deletedBook = await context.Books.FindAsync(book.Id);
            Assert.Null(deletedBook);
        }

        [Fact]
        public async Task GetAllBooks_Should_Return_All_Books()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new BookService(context, _mockMapper.Object);
            var books = new List<Book>
        {
            new Book { Title = "Book 1", PageCount = 100, PublishDate = new DateTime(1990, 1, 1), AuthorId = 1, GenreId = 1 },
            new Book { Title = "Book 2", PageCount = 200, PublishDate = new DateTime(2000, 1, 1), AuthorId = 2, GenreId = 2 }
        };
            context.Books.AddRange(books);
            await context.SaveChangesAsync();

            var resultBooks = new List<ResultBookDto>
        {
            new ResultBookDto { Id = 1, Title = "Book 1", PageCount = 100, PublishDate = new DateTime(1990, 1, 1), AuthorId = 1, GenreId = 1 },
            new ResultBookDto { Id = 2, Title = "Book 2", PageCount = 200, PublishDate = new DateTime(2000, 1, 1), AuthorId = 2, GenreId = 2 }
        };

            _mockMapper.Setup(m => m.Map<List<ResultBookDto>>(It.IsAny<List<Book>>())).Returns(resultBooks);

            // Act
            var result = await service.GetAllBooks();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, b => b.Title == "Book 1");
            Assert.Contains(result, b => b.Title == "Book 2");
        }

        [Fact]
        public async Task GetBookById_Should_Return_Book_When_Book_Exists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new BookService(context, _mockMapper.Object);
            var book = new Book { Title = "Test Book", PageCount = 100, PublishDate = new DateTime(1990, 1, 1), AuthorId = 1, GenreId = 1 };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            var getBookByIdDto = new GetBookByIdDto { Id = book.Id, Title = book.Title, PageCount = book.PageCount, PublishDate = book.PublishDate, AuthorId = book.AuthorId, GenreId = book.GenreId };

            _mockMapper.Setup(m => m.Map<GetBookByIdDto>(It.IsAny<Book>())).Returns(getBookByIdDto);

            // Act
            var result = await service.GetBookById(book.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(book.Title, result.Title);
        }

        [Fact]
        public async Task GetBookByIdBook_Should_Throw_NotFoundException_When_Book_Does_Not_Exist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new BookService(context, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.GetBookById(1));
        }

        [Fact]
        public async Task UpdateBook_Should_Update_Book_When_Book_Exists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new BookService(context, _mockMapper.Object);
            var book = new Book { Title = "Original Title", PageCount = 100, PublishDate = new DateTime(1990, 1, 1), AuthorId = 1, GenreId = 1 };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            var updateBookDto = new UpdateBookDto { Id = book.Id, Title = "Updated Title", PublishDate = new DateTime(2000, 1, 1), PageCount = 150, AuthorId = 2, GenreId = 2 };

            _mockMapper.Setup(m => m.Map(updateBookDto, book)).Callback<UpdateBookDto, Book>((src, dest) =>
            {
                dest.Title = src.Title;
                dest.PageCount = src.PageCount;
                dest.PublishDate = src.PublishDate;
                dest.AuthorId = src.AuthorId;
                dest.GenreId = src.GenreId;
            });

            // Act
            await service.UpdateBook(updateBookDto);

            // Assert
            var updatedBook = await context.Books.FindAsync(book.Id);
            Assert.NotNull(updatedBook);
            Assert.Equal("Updated Title", updatedBook.Title);
            Assert.Equal(150, updatedBook.PageCount);
            Assert.Equal(new DateTime(2000, 1, 1), updatedBook.PublishDate);
            Assert.Equal(2, updatedBook.AuthorId);
            Assert.Equal(2, updatedBook.GenreId);
        }

        [Fact]
        public async Task UpdateBook_Should_Throw_NotFoundException_When_Book_Does_Not_Exist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new BookService(context, _mockMapper.Object);
            var updateBookDto = new UpdateBookDto { Id = 1, Title = "Updated Title", PageCount = 150, PublishDate = new DateTime(2000, 1, 1), AuthorId = 2, GenreId = 2 };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.UpdateBook(updateBookDto));
        }
    }
}