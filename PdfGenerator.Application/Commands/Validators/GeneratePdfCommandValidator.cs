using FluentValidation;
using PdfGenerator.Application.Queries;

namespace PdfGenerator.Application.Commands.Validators;

public class GeneratePdfCommandValidator : AbstractValidator<GeneratePdfQuery>
{
    public  GeneratePdfCommandValidator()
    {
        RuleFor(v => v.GeneratePdfModel.TemplateName)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);
 
        RuleFor(x => x.GeneratePdfModel.Context)
            .NotEmpty().WithMessage("Content is required.");
    }
}