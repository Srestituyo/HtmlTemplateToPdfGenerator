using FluentValidation;

namespace PdfGenerator.Application.Commands.Validators;

public class UpdateHtmlTemplateCommandValidator : AbstractValidator<UpdateHtmlTemplateCommand>
{
    public UpdateHtmlTemplateCommandValidator() 
    {
        RuleFor(v => v.HtmlTemplate.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);
 
        RuleFor(x => x.HtmlTemplate.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
}