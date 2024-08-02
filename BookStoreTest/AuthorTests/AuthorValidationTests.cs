using CohortsBookStore.DTOs.AuthorDtos;
using CohortsBookStore.Validation.AuthorValidator;
using FluentValidation.TestHelper;

namespace BookStoreTests.AuthorTests
{
    public class AuthorValidatorTests 
    {
        private readonly CreateAuthorValidator _createAuthorValidator;
        private readonly DeleteAuthorValidator _deleteAuthorValidator;
        private readonly UpdateAuthorValidator _updateAuthorValidator;
        private readonly GetAuthorByIdValidator _getAuthorByIdValidator;

        public AuthorValidatorTests()
        {
            _createAuthorValidator = new CreateAuthorValidator();
            _deleteAuthorValidator = new DeleteAuthorValidator();
            _updateAuthorValidator = new UpdateAuthorValidator();
            _getAuthorByIdValidator = new GetAuthorByIdValidator();
        }

        [Fact]
        public void CreateAuthorValidator_Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new CreateAuthorDto { Name = "", Surname = "Ates", BirthDate = new DateTime(1999, 12, 18) };
            var result = _createAuthorValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateAuthorValidator_Should_Have_Error_When_Name_Is_Less_Than_3_Characters()
        {
            var model = new CreateAuthorDto { Name = "Al", Surname = "Ates", BirthDate = new DateTime(1999, 12, 18) };
            var result = _createAuthorValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateAuthorValidator_Should_Not_Have_Error_When_Name_Is_Valid()
        {
            var model = new CreateAuthorDto { Name = "Ali", Surname = "Ates", BirthDate = new DateTime(1999, 12, 18) };
            var result = _createAuthorValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // DeleteAuthorValidator Tests
        [Fact]
        public void DeleteAuthorValidator_Should_Have_Error_When_Id_Is_Zero()
        {
            var result = _deleteAuthorValidator.TestValidate(0);
            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void DeleteAuthorValidator_Should_Have_Error_When_Id_Is_Negative()
        {
            var result = _deleteAuthorValidator.TestValidate(-1);
            result.ShouldHaveValidationErrorFor(x => x);
        }

        // UpdateAuthorValidator Tests
        [Fact]
        public void UpdateAuthorValidator_Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new UpdateAuthorDto { Id = 1, Name = "", Surname = "Ates", BirthDate = new DateTime(1999, 12, 18) };
            var result = _updateAuthorValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void UpdateAuthorValidator_Should_Have_Error_When_Id_Is_Zero()
        {
            var model = new UpdateAuthorDto { Id = 0, Name = "Ali", Surname = "Ates", BirthDate = new DateTime(1999, 12, 18) };
            var result = _updateAuthorValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void UpdateAuthorValidator_Should_Not_Have_Error_When_Data_Is_Valid()
        {
            var model = new UpdateAuthorDto { Id = 1, Name = "Ali", Surname = "Ates", BirthDate = new DateTime(1999, 12, 18) };
            var result = _updateAuthorValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
            result.ShouldNotHaveValidationErrorFor(x => x.Surname);
            result.ShouldNotHaveValidationErrorFor(x => x.BirthDate);
        }

        // GetAuthorByIdValidator Tests
        [Fact]
        public void GetAuthorByIdValidator_Should_Have_Error_When_Id_Is_Zero()
        {
            var result = _getAuthorByIdValidator.TestValidate(0);
            result.ShouldHaveValidationErrorFor(x => x).WithErrorMessage("Id değeri 0'dan büyük olmalıdır !");
        }
    }
}