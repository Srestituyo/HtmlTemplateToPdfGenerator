using MediatR;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.ViewModels;

namespace PdfGenerator.Application.Queries;

public class GetAllHtmlTemplateQuery : IRequest<Response<HtmlTemplateViewModel>>
{
    
}