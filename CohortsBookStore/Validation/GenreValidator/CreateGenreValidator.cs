using CohortsBookStore.DTOs.GenreDtos;
using FluentValidation;

namespace CohortsBookStore.Validation.GenreValidator;

public class CreateGenreValidator : AbstractValidator<CreateGenreDto>
{
    public CreateGenreValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().Length(3,30).WithMessage("Name must be between 3-30 characters !");
        RuleFor(x => x.IsActive)
            .NotEmpty();
    }
}