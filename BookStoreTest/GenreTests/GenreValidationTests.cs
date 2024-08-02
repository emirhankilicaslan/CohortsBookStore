using CohortsBookStore.DTOs.GenreDtos;
using CohortsBookStore.Validation.GenreValidator;

namespace BookStoreTest.GenreTests
{
    public class GenreValidatorTests
    {
        private readonly CreateGenreValidator _createGenreValidator;
        private readonly DeleteGenreValidator _deleteGenreValidator;
        private readonly GetGenreByIdValidator _getGenreByIdValidator;
        private readonly UpdateGenreValidator _updateGenreValidator;

        public GenreValidatorTests()
        {
            _createGenreValidator = new CreateGenreValidator();
            _deleteGenreValidator = new DeleteGenreValidator();
            _getGenreByIdValidator = new GetGenreByIdValidator();
            _updateGenreValidator = new UpdateGenreValidator();
        }

        [Fact]
        public void CreateGenreValidator_Should_Pass_When_Valid_Data()
        {
            // Arrange
            var dto = new CreateGenreDto { Name = "Valid Genre" };

            // Act
            var result = _createGenreValidator.Validate(dto);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateGenreValidator_Should_Fail_When_Name_Is_Empty()
        {
            // Arrange
            var dto = new CreateGenreDto { Name = "" };

            // Act
            var result = _createGenreValidator.Validate(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage == "Name must be between 3-30 characters !");
        }

        [Fact]
        public void CreateGenreValidator_Should_Fail_When_Name_Is_Too_Short()
        {
            // Arrange
            var dto = new CreateGenreDto { Name = "AA" };

            // Act
            var result = _createGenreValidator.Validate(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage == "Name must be between 3-30 characters !");
        }

        [Fact]
        public void DeleteGenreValidator_Should_Pass_When_Valid_Id()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _deleteGenreValidator.Validate(id);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void DeleteGenreValidator_Should_Fail_When_Id_Is_Zero()
        {
            // Arrange
            var id = 0;

            // Act
            var result = _deleteGenreValidator.Validate(id);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void DeleteGenreValidator_Should_Fail_When_Id_Is_Negative()
        {
            // Arrange
            var id = -1;

            // Act
            var result = _deleteGenreValidator.Validate(id);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void GetGenreByIdValidator_Should_Pass_When_Valid_Id()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _getGenreByIdValidator.Validate(id);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetGenreByIdValidator_Should_Fail_When_Id_Is_Zero()
        {
            // Arrange
            var id = 0;

            // Act
            var result = _getGenreByIdValidator.Validate(id);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void GetGenreByIdValidator_Should_Fail_When_Id_Is_Negative()
        {
            // Arrange
            var id = -1;

            // Act
            var result = _getGenreByIdValidator.Validate(id);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void UpdateGenreValidator_Should_Pass_When_Valid_Data()
        {
            // Arrange
            var dto = new UpdateGenreDto { Id = 1, Name = "Valid Genre" };

            // Act
            var result = _updateGenreValidator.Validate(dto);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void UpdateGenreValidator_Should_Fail_When_Name_Is_Empty()
        {
            // Arrange
            var dto = new UpdateGenreDto { Id = 1, Name = "" };

            // Act
            var result = _updateGenreValidator.Validate(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage == "Name must be between 3-30 characters !");
        }

        [Fact]
        public void UpdateGenreValidator_Should_Fail_When_Name_Is_Too_Short()
        {
            // Arrange
            var dto = new UpdateGenreDto { Id = 1, Name = "AA" };

            // Act
            var result = _updateGenreValidator.Validate(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage == "Name must be between 3-30 characters !");
        }
    }
}