using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.DTOs;
using PdfGenerator.Application.Queries;
using PdfGenerator.Infrastructure;

namespace PdfGenerator.Application.Handlers;

public class GetHtmlTemplateByNameHandler : IRequestHandler<GetHtmlTemplateByNameQuery, Response<HtmlTemplateDTO>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public GetHtmlTemplateByNameHandler(DataContext theContext, IMapper theMapper)
    {
        _dataContext = theContext;
        _mapper = theMapper;
    }

    public async Task<Response<HtmlTemplateDTO>> Handle(GetHtmlTemplateByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var aHtmlTemplate = await _dataContext.HtmlTemplates
                .AsNoTracking()
                .Where(x => x.Name == request.Name)
                .ProjectTo<HtmlTemplateDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (aHtmlTemplate == null)
            {
                throw new KeyNotFoundException($"The HTML template with the given name {request.Name} was not found.");
            }

            return new Response<HtmlTemplateDTO>(aHtmlTemplate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}