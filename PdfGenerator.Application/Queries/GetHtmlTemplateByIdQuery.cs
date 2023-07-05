using MediatR;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.DTOs;

namespace PdfGenerator.Application.Queries;

public class GetHtmlTemplateByIdQuery : IRequest<Response<HtmlTemplateDTO>>
{
    public Guid Id { get; set; }
}