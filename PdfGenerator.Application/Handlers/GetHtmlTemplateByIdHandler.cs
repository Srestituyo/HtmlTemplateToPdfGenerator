using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.DTOs;
using PdfGenerator.Application.Queries;
using PdfGenerator.Infrastructure;
using Serilog;

namespace PdfGenerator.Application.Handlers;

public class GetHtmlTemplateByIdHandler : IRequestHandler<GetHtmlTemplateByIdQuery, Response<HtmlTemplateDTO>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public GetHtmlTemplateByIdHandler(DataContext theContext, IMapper theMapper)
    {
        _dataContext = theContext;
        _mapper = theMapper;
    }

    public async Task<Response<HtmlTemplateDTO>> Handle(GetHtmlTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var aHtmlTemplate = await _dataContext.HtmlTemplates
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .ProjectTo<HtmlTemplateDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (aHtmlTemplate == null)
            {
                throw new KeyNotFoundException($"The HTML template with the given ID {request.Id} was not found.");
            }

            return new Response<HtmlTemplateDTO>(aHtmlTemplate);
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw;
        }
    }
}