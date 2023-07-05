using MediatR;
using PdfGenerator.Application.Common.Wrapper;

namespace PdfGenerator.Application.Commands;

public class RemoveHtmlTemplateByNameCommand : IRequest<Response<Guid>>
{
    public string Name { get; set; }
}