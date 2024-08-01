using CohortsBookStore.DTOs.GenreDtos;
using FluentValidation;

namespace CohortsBookStore.Validation.GenreValidator;

public class CreateGenreValidator : AbstractValidator<CreateGenreDto>
{
    public CreateGenreValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MinimumLength(2);
        RuleFor(x => x.IsActive)
            .NotEmpty();
    }
}