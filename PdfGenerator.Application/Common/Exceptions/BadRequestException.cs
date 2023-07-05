using FluentValidation.Results;

namespace PdfGenerator.Application.Common.Exceptions;

public class BadRequestException : Exception
{
    
    public BadRequestException()
        : base("One or more validation failures have occured.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public BadRequestException(string message)
        : base(message)
    {
    }

    public BadRequestException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}