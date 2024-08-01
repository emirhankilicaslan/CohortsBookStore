using CohortsBookStore.DTOs.AuthorDtos;
using FluentValidation;

namespace CohortsBookStore.Validation.AuthorValidator;

public class ByIdAuthorValidator : AbstractValidator<ByIdAuthorDto>
{
    public ByIdAuthorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}