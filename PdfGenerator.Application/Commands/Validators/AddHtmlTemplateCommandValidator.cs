using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Infrastructure;

namespace PdfGenerator.Application.Commands.Validators;

public class AddHtmlTemplateCommandValidator : AbstractValidator<AddHtmlTemplateCommand>
{
     
    public AddHtmlTemplateCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);
 
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
    
}