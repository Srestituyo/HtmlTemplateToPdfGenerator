using MediatR;
using PdfGenerator.Application.Common.Wrapper;

namespace PdfGenerator.Application.Commands;

public class RemoveHtmlTemplateByIdCommand : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }
}