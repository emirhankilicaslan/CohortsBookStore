using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTOs.GenreDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Exceptions;
using CohortsBookStore.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookStoreTest.GenreTests
{
    public class GenreServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;

        public GenreServiceTests()
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
        public async Task CreateGenre_Should_Add_Genre()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new GenreService(context, _mockMapper.Object);
            var createGenreDto = new CreateGenreDto { Name = "Test Genre" };
            var genre = new Genre { Name = "Test Genre" };
            _mockMapper.Setup(m => m.Map<Genre>(createGenreDto)).Returns(genre);

            // Act
            await service.CreateGenre(createGenreDto);

            // Assert
            var addedGenre = await context.Genres.FirstOrDefaultAsync(g => g.Name == "Test Genre");
            Assert.NotNull(addedGenre);
            Assert.Equal("Test Genre", addedGenre.Name);
        }

        [Fact]
        public async Task DeleteGenre_Should_Remove_Genre_When_Genre_Exists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new GenreService(context, _mockMapper.Object);
            var genre = new Genre { Name = "Test Genre" };
            context.Genres.Add(genre);
            await context.SaveChangesAsync();

            // Act
            await service.DeleteGenre(genre.Id);

            // Assert
            var deletedGenre = await context.Genres.FindAsync(genre.Id);
            Assert.Null(deletedGenre);
        }

        [Fact]
        public async Task GetAllGenres_Should_Return_All_Genres()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new GenreService(context, _mockMapper.Object);
            var genres = new List<Genre>
        {
            new Genre { Name = "Genre 1" },
            new Genre { Name = "Genre 2" }
        };
            context.Genres.AddRange(genres);
            await context.SaveChangesAsync();
            var resultGenres = genres.Select(g => new ResultGenreDto { Id = g.Id, Name = g.Name }).ToList();
            _mockMapper.Setup(m => m.Map<List<ResultGenreDto>>(genres)).Returns(resultGenres);

            // Act
            var result = await service.GetAllGenres();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Name == "Genre 1");
            Assert.Contains(result, g => g.Name == "Genre 2");
        }

        [Fact]
        public async Task GetGenreById_Should_Return_Genre_When_Genre_Exists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new GenreService(context, _mockMapper.Object);
            var genre = new Genre { Name = "Test Genre" };
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            var resultGenre = new GetGenreByIdDto { Id = genre.Id, Name = genre.Name };
            _mockMapper.Setup(m => m.Map<GetGenreByIdDto>(genre)).Returns(resultGenre);

            // Act
            var result = await service.GetGenreById(genre.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(genre.Name, result.Name);
        }

        [Fact]
        public async Task GetGenreById_Should_Throw_NotFoundException_When_Genre_Does_Not_Exist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new GenreService(context, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.GetGenreById(1));
        }

        [Fact]
        public async Task UpdateGenre_Should_Update_Genre_When_Genre_Exists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new GenreService(context, _mockMapper.Object);
            var genre = new Genre { Name = "Original Name"};
            context.Genres.Add(genre);
            await context.SaveChangesAsync();

            var updateGenreDto = new UpdateGenreDto { Id = genre.Id, Name = "Updated Name" , IsActive = false};
            _mockMapper.Setup(m => m.Map(updateGenreDto, genre)).Callback(() =>
            {
                genre.Name = updateGenreDto.Name;
                genre.IsActive = updateGenreDto.IsActive;
            });

            // Act
            await service.UpdateGenre(updateGenreDto);

            // Assert
            var updatedGenre = await context.Genres.FindAsync(genre.Id);
            Assert.NotNull(updatedGenre);
            Assert.Equal("Updated Name", updatedGenre.Name);
            Assert.False(updatedGenre.IsActive);
        }

        [Fact]
        public async Task UpdateGenre_Should_Throw_NotFoundException_When_Genre_Does_Not_Exist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new GenreService(context, _mockMapper.Object);
            var updateGenreDto = new UpdateGenreDto { Id = 1, Name = "Updated Name" , IsActive = false};

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.UpdateGenre(updateGenreDto));
        }
    }
}