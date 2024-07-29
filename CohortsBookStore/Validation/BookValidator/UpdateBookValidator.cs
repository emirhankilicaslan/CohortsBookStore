using CohortsBookStore.DTO_s.BookDtos;
using FluentValidation;

namespace CohortsBookStore.Validation;

public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
{
    public UpdateBookValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.Title)
            .NotEmpty().MinimumLength(2);
        RuleFor(x => x.GenreId)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.PublishDate)
            .NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(x => x.PageCount)
            .NotEmpty().GreaterThan(0);
    }
}