using CohortsBookStore.DTOs.GenreDtos;
using FluentValidation;

namespace CohortsBookStore.Validation.GenreValidator;

public class GetGenreByIdValidator : AbstractValidator<int>
{
    public GetGenreByIdValidator()
    {
        RuleFor(x => x)
            .NotEmpty().GreaterThan(0);
    }
}