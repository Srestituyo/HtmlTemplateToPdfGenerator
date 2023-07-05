using MediatR;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.DTOs;

namespace PdfGenerator.Application.Queries;

public class GetHtmlTemplateByNameQuery : IRequest<Response<HtmlTemplateDTO>>
{
    public string Name { get; set; }
}