using CohortsBookStore.DTO_s.BookDtos;
using CohortsBookStore.Validation;

namespace BookStoreTest.BookTests
{
    public class BookValidatorTests
    {
        private readonly CreateBookValidator _createBookValidator;
        private readonly DeleteBookValidator _deleteBookValidator;
        private readonly GetBookByIdValidator _getBookByIdValidator;
        private readonly UpdateBookValidator _updateBookValidator;

        public BookValidatorTests()
        {
            _createBookValidator = new CreateBookValidator();
            _deleteBookValidator = new DeleteBookValidator();
            _getBookByIdValidator = new GetBookByIdValidator();
            _updateBookValidator = new UpdateBookValidator();
        }

        [Fact]
        public void CreateBookValidator_Should_Pass_When_Valid_Data()
        {
            // Arrange
            var dto = new CreateBookDto { Title = "Valid Title", PageCount = 150, PublishDate = new DateTime(1990, 1,1), AuthorId = 1, GenreId = 1 };

            // Act
            var result = _createBookValidator.Validate(dto);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateBookValidator_Should_Fail_When_Title_Is_Empty()
        {
            // Arrange
            var dto = new CreateBookDto { Title = "", PageCount = 150, PublishDate = new DateTime(1990, 1,1), AuthorId = 1, GenreId = 1 };

            // Act
            var result = _createBookValidator.Validate(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Title");
        }

        [Fact]
        public void CreateBookValidator_Should_Fail_When_PageCount_Is_Out_Of_Range()
        {
            // Arrange
            var dto = new CreateBookDto { Title = "Valid Title", PageCount = 3000, PublishDate = new DateTime(1990, 1,1), AuthorId = 1, GenreId = 1 };

            // Act
            var result = _createBookValidator.Validate(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "PageCount");
        }

        [Fact]
        public void DeleteBookValidator_Should_Pass_When_Valid_Id()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _deleteBookValidator.Validate(id);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void DeleteBookValidator_Should_Fail_When_Id_Is_Zero()
        {
            // Arrange
            var id = 0;

            // Act
            var result = _deleteBookValidator.Validate(id);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void DeleteBookValidator_Should_Fail_When_Id_Is_Negative()
        {
            // Arrange
            var id = -1;

            // Act
            var result = _deleteBookValidator.Validate(id);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void GetBookByIdValidator_Should_Pass_When_Valid_Id()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _getBookByIdValidator.Validate(id);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetBookByIdValidator_Should_Fail_When_Id_Is_Zero()
        {
            // Arrange
            var id = 0;

            // Act
            var result = _getBookByIdValidator.Validate(id);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void GetBookByIdValidator_Should_Fail_When_Id_Is_Negative()
        {
            // Arrange
            var id = -1;

            // Act
            var result = _getBookByIdValidator.Validate(id);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void UpdateBookValidator_Should_Pass_When_Valid_Data()
        {
            // Arrange
            var dto = new UpdateBookDto { Id = 1, Title = "Valid Title", PageCount = 150, PublishDate = new DateTime(1990, 1,1), AuthorId = 1, GenreId = 1 };

            // Act
            var result = _updateBookValidator.Validate(dto);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void UpdateBookValidator_Should_Fail_When_Title_Is_Empty()
        {
            // Arrange
            var dto = new UpdateBookDto { Id = 1, Title = "", PageCount = 150, PublishDate = new DateTime(1990, 1,1), AuthorId = 1, GenreId = 1 };

            // Act
            var result = _updateBookValidator.Validate(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Title");
        }

        [Fact]
        public void UpdateBookValidator_Should_Fail_When_PageCount_Is_Out_Of_Range()
        {
            // Arrange
            var dto = new UpdateBookDto { Id = 1, Title = "Valid Title", PageCount = 3000, PublishDate = new DateTime(1990, 1,1), AuthorId = 1, GenreId = 1 };

            // Act
            var result = _updateBookValidator.Validate(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "PageCount");
        }
    }
}