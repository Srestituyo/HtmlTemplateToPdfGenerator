using MediatR;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.Models;

namespace PdfGenerator.Application.Queries;

public class GeneratePdfQuery : IRequest<Response<string>>
{
    public GeneratePdfModel GeneratePdfModel { get; set; }
}