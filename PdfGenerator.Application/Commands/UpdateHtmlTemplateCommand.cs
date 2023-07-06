using MediatR;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.Models;

namespace PdfGenerator.Application.Commands;

public class UpdateHtmlTemplateCommand : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }

    public HtmlTemplateModel HtmlTemplate { get; set; }

    public UpdateHtmlTemplateCommand(Guid id, HtmlTemplateModel htmlTemplate)
    {
        Id = id;
        HtmlTemplate = htmlTemplate;
    }
}