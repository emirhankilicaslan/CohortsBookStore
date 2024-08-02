using CohortsBookStore.DTOs.AuthorDtos;
using FluentValidation;

namespace CohortsBookStore.Validation.AuthorValidator;

public class GetAuthorByIdValidator : AbstractValidator<int>
{
    public GetAuthorByIdValidator()
    {
        RuleFor(x => x)
            .NotEmpty().GreaterThan(0).WithMessage("Id değeri 0'dan büyük olmalıdır !");
    }
}