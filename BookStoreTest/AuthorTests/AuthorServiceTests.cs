using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTOs.AuthorDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookStoreTests.AuthorTests
{
    public class AuthorServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;

        public AuthorServiceTests()
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
        public async Task GetAllAuthors_Should_Return_All_Authors()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var authorService = new AuthorService(context, _mockMapper.Object);

            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "Ali", Surname = "Ates", BirthDate = new DateTime(1999, 12, 18) },
                new Author { Id = 2, Name = "Veli", Surname = "Duran", BirthDate = new DateTime(1996, 8, 2) }
            };

            context.Authors.AddRange(authors);
            await context.SaveChangesAsync();

            var resultAuthors = new List<ResultAuthorDto>
            {
                new ResultAuthorDto { Id = 1, Name = "Ali", Surname = "Ates" },
                new ResultAuthorDto { Id = 2, Name = "Veli", Surname = "Duran" }
            };

            _mockMapper.Setup(m => m.Map<List<ResultAuthorDto>>(authors)).Returns(resultAuthors);

            // Act
            var result = await authorService.GetAllAuthors();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Ali", result[0].Name);
            Assert.Equal("Veli", result[1].Name);
        }

        [Fact]
        public async Task GetAuthorById_Should_Return_Author_By_Id()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var authorService = new AuthorService(context, _mockMapper.Object);

            var authorId = 1;
            var author = new Author 
            { 
                Id = authorId, 
                Name = "Ali", 
                Surname = "Ates", 
                BirthDate = new DateTime(1999, 12, 18) 
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var resultAuthor = new GetAuthorByIdDto 
            { 
                Id = authorId, 
                Name = "Ali", 
                Surname = "Ates" 
            };

            _mockMapper.Setup(m => m.Map<GetAuthorByIdDto>(author)).Returns(resultAuthor);

            // Act
            var result = await authorService.GetAuthorById(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ali", result.Name);
            Assert.Equal("Ates", result.Surname);
        }
        
        [Fact]
        public async Task CreateAuthor_Should_Create_Author()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var authorService = new AuthorService(context, _mockMapper.Object);

            var createAuthorDto = new CreateAuthorDto
            { 
                Name = "Ali", 
                Surname = "Ates", 
                BirthDate = new DateTime(1999, 12, 18) 
            };
            
            var author = new Author 
            { 
                Id = 1, 
                Name = "Ali", 
                Surname = "Ates", 
                BirthDate = new DateTime(1999, 12, 18) 
            };

            _mockMapper.Setup(m => m.Map<Author>(createAuthorDto)).Returns(author);

            // Act
            await authorService.CreateAuthor(createAuthorDto);

            // Assert
            var createdAuthor = await context.Authors.FirstOrDefaultAsync(a => a.Name == "Ali" && a.Surname == "Ates");
            Assert.NotNull(createdAuthor);
            Assert.Equal("Ali", createdAuthor.Name);
            Assert.Equal("Ates", createdAuthor.Surname);
            Assert.Equal(new DateTime(1999, 12, 18), createdAuthor.BirthDate);
        }

        [Fact]
        public async Task DeleteAuthor_Should_Delete_Author()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var authorService = new AuthorService(context, _mockMapper.Object);

            var authorId = 1;
            var author = new Author { Id = authorId, Name = "Ali", Surname = "Ates", BirthDate = new DateTime(1999, 12, 18) };

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            // Act
            await authorService.DeleteAuthor(authorId);

            // Assert
            var deletedAuthor = await context.Authors.FindAsync(authorId);
            Assert.Null(deletedAuthor);
        }
        
        [Fact]
        public async Task UpdateAuthor_Should_Update_Author()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var authorService = new AuthorService(context, _mockMapper.Object);

            var authorId = 1;
            var author = new Author 
            { 
                Id = authorId, 
                Name = "Ali", 
                Surname = "Ates", 
                BirthDate = new DateTime(1999, 12, 18) 
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var updateAuthorDto = new UpdateAuthorDto 
            { 
                Id = authorId, 
                Name = "Ali Updated", 
                Surname = "Ates Updated", 
                BirthDate = new DateTime(1999, 12, 18) 
            };

            author.Id = updateAuthorDto.Id;
            author.Name = updateAuthorDto.Name;
            author.Surname = updateAuthorDto.Surname;
            author.BirthDate = updateAuthorDto.BirthDate;

            // Act
            await authorService.UpdateAuthor(updateAuthorDto);

            // Assert
            var updatedAuthor = await context.Authors.FindAsync(authorId);
            Assert.NotNull(updatedAuthor);
            Assert.Equal("Ali Updated", updatedAuthor.Name);
            Assert.Equal("Ates Updated", updatedAuthor.Surname);
        }
    }
}