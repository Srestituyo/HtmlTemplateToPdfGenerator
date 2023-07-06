using MediatR;
using PdfGenerator.Application.Common.Wrapper;

namespace PdfGenerator.Application.Commands;

public class AddHtmlTemplateCommand : IRequest<Response<Guid>>
{
    public string Name { get; set; }

    public string Content { get; set; }

    public string AdditionalContext { get; set; }

    public AddHtmlTemplateCommand(string name, string content, string additionalContext)
    {
        Name = name;
        Content = content;
        AdditionalContext = additionalContext;
    }

}