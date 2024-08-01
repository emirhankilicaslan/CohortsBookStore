using CohortsBookStore.DTOs.GenreDtos;
using FluentValidation;

namespace CohortsBookStore.Validation.GenreValidator;

public class ByIdGenreValidator : AbstractValidator<ByIdGenreDto>
{
    public ByIdGenreValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}