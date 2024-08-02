using CohortsBookStore.DTO_s.BookDtos;
using FluentValidation;

namespace CohortsBookStore.Validation;

public class GetBookByIdValidator : AbstractValidator<int>
{
    public GetBookByIdValidator()
    {
        RuleFor(x => x)
            .NotEmpty().GreaterThan(0);
    }
}